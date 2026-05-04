using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api[controller]")]
    public class LotesController : ControllerBase
    {
        private readonly AppDbContext context;

        public LotesController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpPost("sincronizar")]
        public async Task<IActionResult> SincronizarLotes()
        {
            try
            {
                var UbicacionDefectoId = await (
                    from ua in context.UbicacionesAlmacen
                    where ua.Codigo == "UBIC-REC-001"
                    select ua.Id
                ).FirstOrDefaultAsync();

                if (UbicacionDefectoId == 0)
                    return BadRequest("No se encontro la ubicacion por defecto");

                var pendientes = await (
                    from dr in context.DetallesRecepcion
                    where
                        !(from l in context.Lotes select l.DetalleRecepcionId).Contains(dr.Id)
                        && dr.Estado != "Dañado"
                        && dr.Estado != "Rechazado"
                        && dr.CantidadRecibida > 0
                    select dr
                ).ToListAsync();

                List<string> codigos = new List<string>();

                foreach (var d in pendientes)
                {
                    var nuevoLote = new Lote
                    {
                        Codigo = CodeGeneratorService.GenerateLoteCode(d.FechaVencimiento),
                        MedicamentoId = d.MedicamentoId,
                        DetalleRecepcionId = d.Id,
                        CantidadInicial = d.CantidadRecibida,
                        FechaIngreso = DateOnly.FromDateTime(DateTime.Now),
                        Estado = "Disponible",
                    };
                    context.Lotes.Add(nuevoLote);

                    var nuevoStock = new StockActual
                    {
                        Lote = nuevoLote,
                        UbicacionId = UbicacionDefectoId,
                        Cantidad = nuevoLote.CantidadInicial,
                        Estado = "Activo",
                    };

                    context.StocksActuales.Add(nuevoStock);

                    var movimiento = new Movimiento
                    {
                        Codigo = CodeGeneratorService.GenerateMovimientoCode(),
                        StockActual = nuevoStock,
                        TipoMovimientoId = 5,
                        Cantidad = nuevoStock.Cantidad,
                        EntidadReferenciaId = d.RecepcionId,
                        Fecha = DateOnly.FromDateTime(DateTime.Now),
                        Estado = "Activo",
                    };

                    context.Movimientos.Add(movimiento);

                    codigos.Add(nuevoLote.Codigo);
                }

                await context.SaveChangesAsync();

                return Ok(
                    new
                    {
                        mensaje = "Sincronizacion completa",
                        cantidad = pendientes.Count,
                        codigosLotes = codigos,
                    }
                );
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.ToString()}");
            }
        }
    }
}
