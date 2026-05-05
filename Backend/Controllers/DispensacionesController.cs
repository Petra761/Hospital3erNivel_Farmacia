using Data;
using DTOs;
using DTOs.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DispensacionesController : ControllerBase
    {
        private readonly AppDbContext context;

        public DispensacionesController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostDispensacion(DispensacionPostDto dispensacion)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var receta = await context
                    .Recetas.Include(r => r.Detalles)
                    .FirstOrDefaultAsync(r =>
                        r.Estado != "Entregado"
                        && r.Estado != "Eliminado"
                        && r.Codigo == dispensacion.RecetaCodigo
                    );

                if (receta == null)
                    return NotFound($"No se encontró la receta {dispensacion.RecetaCodigo}");

                var tipoMovimiento = await context.TiposMovimientos.FirstOrDefaultAsync(tm =>
                    tm.Estado != "Eliminado" && tm.Descripcion == "Receta"
                );

                if (tipoMovimiento == null)
                    return BadRequest("Tipo de movimiento no encontrado");

                var nDispensacion = new Dispensacion
                {
                    Codigo = CodeGeneratorService.GenerateDispensacionCode(),
                    RecetaId = receta.Id,
                    FarmaceuticoCodigo = dispensacion.FarmaceuticoIdentificador,
                    Fecha = DateOnly.FromDateTime(DateTime.Now),
                    Estado = "Completado",
                };

                context.Dispensaciones.Add(nDispensacion);

                foreach (var detalle in receta.Detalles)
                {
                    int cantidadRestante = detalle.CantidadSolicitada;

                    var stockDisponible = await context
                        .StocksActuales.Include(s => s.Lote)
                        .Where(s => s.Lote.MedicamentoId == detalle.MedicamentoId && s.Cantidad > 0)
                        .OrderBy(s => s.Lote.DetalleRecepcion.FechaVencimiento)
                        .ToListAsync();

                    foreach (var stock in stockDisponible)
                    {
                        if (cantidadRestante <= 0)
                            break;

                        int cantidadASacar = Math.Min(stock.Cantidad, cantidadRestante);

                        context.DispensacionesLote.Add(
                            new DispensacionLote
                            {
                                Dispensacion = nDispensacion,
                                StockActualId = stock.Id,
                                DetalleRecetaId = detalle.Id,
                                CantidadEntregada = cantidadASacar,
                                Estado = "Entregado",
                            }
                        );

                        stock.Cantidad -= cantidadASacar;

                        context.Movimientos.Add(
                            new Movimiento
                            {
                                Codigo = CodeGeneratorService.GenerateMovimientoCode(),
                                StockActualId = stock.Id,
                                TipoMovimientoId = tipoMovimiento.Id,
                                Cantidad = cantidadASacar,
                                EntidadReferenciaId = receta.Id,
                                Fecha = DateOnly.FromDateTime(DateTime.Now),
                                Estado = "Activo",
                            }
                        );

                        cantidadRestante -= cantidadASacar;
                    }

                    detalle.Estado =
                        cantidadRestante == 0 ? "Entregado Total" : "Entregado Parcial";
                }

                receta.Estado = receta.Detalles.All(d => d.Estado == "Entregado Total")
                    ? "Entregado"
                    : "Entregado Parcial";

                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(
                    new
                    {
                        mensaje = "Dispensación realizada con éxito",
                        codigo = nDispensacion.Codigo,
                    }
                );
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(ex.ToString());
            }
        }

        [HttpPost("enfermeria")]
        public async Task<IActionResult> PostDispensacionDesdeEnfermeria(
            DispensacionEnfermeriaDto dto
        )
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                var receta = await context
                    .Recetas.Include(r => r.Detalles)
                    .FirstOrDefaultAsync(r =>
                        r.Codigo == dto.RecetaCodigo
                        && r.Estado != "Entregado"
                        && r.Estado != "Eliminado"
                    );

                if (receta == null)
                    return NotFound(
                        $"La receta {dto.RecetaCodigo} no está disponible para dispensación (ya entregada o no existe)."
                    );

                var tipoMovimiento = await context.TiposMovimientos.FirstOrDefaultAsync(tm =>
                    tm.Descripcion.Contains("Salida") || tm.Descripcion.Contains("Receta")
                );

                if (tipoMovimiento == null)
                    return BadRequest(
                        "Error de configuración: No se encontró el tipo de movimiento 'Salida' en el sistema."
                    );

                var nDispensacion = new Dispensacion
                {
                    Codigo = CodeGeneratorService.GenerateDispensacionCode(),
                    RecetaId = receta.Id,
                    Fecha = DateOnly.FromDateTime(DateTime.Now),
                    Estado = "Completado",

                    FarmaceuticoCodigo = "FARM-SISTEMA-01",
                    QuienRecoge = dto.EnfermeraCodigo,
                };

                context.Dispensaciones.Add(nDispensacion);

                foreach (var detalle in receta.Detalles)
                {
                    int cantidadRestante = detalle.CantidadSolicitada;

                    var stockDisponible = await (
                        from s in context.StocksActuales
                        join l in context.Lotes on s.LoteId equals l.Id
                        join dr in context.DetallesRecepcion on l.DetalleRecepcionId equals dr.Id
                        where l.MedicamentoId == detalle.MedicamentoId && s.Cantidad > 0
                        orderby dr.FechaVencimiento ascending // El que vence primero sale primero
                        select s
                    )
                        .Include(s => s.Lote)
                        .ToListAsync();

                    foreach (var stock in stockDisponible)
                    {
                        if (cantidadRestante <= 0)
                            break;

                        int cantidadASacar = Math.Min(stock.Cantidad, cantidadRestante);

                        context.DispensacionesLote.Add(
                            new DispensacionLote
                            {
                                Dispensacion = nDispensacion,
                                StockActualId = stock.Id,
                                DetalleRecetaId = detalle.Id,
                                CantidadEntregada = cantidadASacar,
                                Estado = "Entregado",
                            }
                        );

                        stock.Cantidad -= cantidadASacar;

                        context.Movimientos.Add(
                            new Movimiento
                            {
                                Codigo = CodeGeneratorService.GenerateMovimientoCode(),
                                StockActualId = stock.Id,
                                TipoMovimientoId = tipoMovimiento.Id,
                                Cantidad = cantidadASacar,
                                EntidadReferenciaId = receta.Id,
                                Fecha = DateOnly.FromDateTime(DateTime.Now),
                                Estado = "Activo",
                                Observaciones =
                                    $"Salida por receta {receta.Codigo} - Recogido por Enf. {dto.EnfermeraCodigo}",
                            }
                        );

                        cantidadRestante -= cantidadASacar;
                    }

                    if (cantidadRestante == detalle.CantidadSolicitada)
                        detalle.Estado = "Sin Stock";
                    else
                        detalle.Estado =
                            (cantidadRestante == 0) ? "Entregado Total" : "Entregado Parcial";
                }

                if (receta.Detalles.All(d => d.Estado == "Entregado Total"))
                    receta.Estado = "Entregado";
                else if (receta.Detalles.Any(d => d.Estado.Contains("Entregado")))
                    receta.Estado = "Entregado Parcial";
                else
                    receta.Estado = "Pendiente (Sin Stock)";

                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(
                    new
                    {
                        mensaje = "Dispensación exitosa para personal de enfermería",
                        receta = receta.Codigo,
                        entrega = nDispensacion.Codigo,
                        estadoFinal = receta.Estado,
                    }
                );
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest($"Error en el proceso de despacho: {ex.Message}");
            }
        }
    }
}
