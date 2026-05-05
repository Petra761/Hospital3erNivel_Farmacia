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
    [Route("api[controller]")]
    public class LotesController : ControllerBase
    {
        private readonly AppDbContext context;

        public LotesController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hoy = DateOnly.FromDateTime(DateTime.Now);

            var lotes = await (
                from l in context.Lotes
                join dr in context.DetallesRecepcion on l.DetalleRecepcionId equals dr.Id
                join m in context.Medicamentos on l.MedicamentoId equals m.Id
                join tm in context.TiposMedicamentos on m.MedicamentoId equals tm.Id

                let stockTotal = (
                    from s in context.StocksActuales
                    where s.LoteId == l.Id
                    select s.Cantidad
                ).Sum()

                orderby dr.FechaVencimiento ascending

                select new LoteReadDto(
                    l.Codigo,
                    tm.NombreGenerico + " (" + tm.NombreComercial + ")",
                    l.FechaIngreso,
                    dr.FechaVencimiento,
                    l.CantidadInicial,
                    stockTotal,
                    dr.FechaVencimiento.DayNumber - hoy.DayNumber,
                    l.Estado
                )
            ).ToListAsync();

            return Ok(lotes);
        }

        [HttpGet("reporte-vencimientos/{dias}")]
        public async Task<IActionResult> GetLotesProximosAVencer(int dias)
        {
            var hoy = DateOnly.FromDateTime(DateTime.Now);
            var fechaLimite = hoy.AddDays(dias);

            var reporte = await (
                from l in context.Lotes
                join dr in context.DetallesRecepcion on l.DetalleRecepcionId equals dr.Id
                join m in context.Medicamentos on l.MedicamentoId equals m.Id
                join tm in context.TiposMedicamentos on m.MedicamentoId equals tm.Id
                join s in context.StocksActuales on l.Id equals s.LoteId
                join u in context.UbicacionesAlmacen on s.UbicacionId equals u.Id

                where dr.FechaVencimiento <= fechaLimite && s.Cantidad > 0
                orderby dr.FechaVencimiento ascending

                select new LoteVencimientoReadDto(
                    l.Codigo,
                    tm.NombreGenerico + " (" + tm.NombreComercial + ")",
                    u.Nombre,
                    s.Cantidad,
                    dr.FechaVencimiento,
                    dr.FechaVencimiento.DayNumber - hoy.DayNumber,
                    (dr.FechaVencimiento < hoy)
                        ? "VENCIDO"
                        : (
                            (dr.FechaVencimiento.DayNumber - hoy.DayNumber) <= 15
                                ? "CRÍTICA"
                                : "ALERTA"
                        )
                )
            ).ToListAsync();

            return Ok(reporte);
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

        [HttpPut("reubicacion")]
        public async Task<IActionResult> TransferirLotesCompletos(MoverLotesDto dto)
        {
            if (dto.LotesCodigos == null || !dto.LotesCodigos.Any())
                return BadRequest("Debe proporcionar al menos un código de lote.");

            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                var uDestino = await (
                    from u in context.UbicacionesAlmacen
                    where u.Codigo == dto.DestinoCodigo
                    select u
                ).FirstOrDefaultAsync();

                if (uDestino == null)
                    return BadRequest($"La ubicación de destino {dto.DestinoCodigo} no existe.");

                var tipoMovId = await (
                    from tm in context.TiposMovimientos
                    where tm.Descripcion.Contains("Reubicacion")
                    select tm.Id
                ).FirstOrDefaultAsync();

                foreach (var loteCodigo in dto.LotesCodigos)
                {
                    var stocksActuales = await (
                        from s in context.StocksActuales
                        join l in context.Lotes on s.LoteId equals l.Id
                        where l.Codigo == loteCodigo && s.Cantidad > 0
                        select s
                    ).ToListAsync();

                    if (!stocksActuales.Any())
                        continue;

                    int loteId = stocksActuales.First().LoteId;

                    var stockDestino = await (
                        from s in context.StocksActuales
                        where s.LoteId == loteId && s.UbicacionId == uDestino.Id
                        select s
                    ).FirstOrDefaultAsync();

                    if (stockDestino == null)
                    {
                        stockDestino = new StockActual
                        {
                            LoteId = loteId,
                            UbicacionId = uDestino.Id,
                            Cantidad = 0,
                            Estado = "Activo",
                        };
                        context.StocksActuales.Add(stockDestino);
                    }

                    foreach (var sOrigen in stocksActuales)
                    {
                        if (sOrigen.UbicacionId == uDestino.Id)
                            continue;

                        int cantidadAMover = sOrigen.Cantidad;

                        sOrigen.Cantidad = 0;
                        stockDestino.Cantidad += cantidadAMover;

                        context.Movimientos.Add(
                            new Movimiento
                            {
                                Codigo = CodeGeneratorService.GenerateMovimientoCode(),
                                StockActual = sOrigen,
                                TipoMovimientoId = tipoMovId,
                                Cantidad = cantidadAMover,
                                EntidadReferenciaId = null,
                                Fecha = DateOnly.FromDateTime(DateTime.Now),
                                Estado = "Vaciado por Reubicación",
                            }
                        );

                        context.Movimientos.Add(
                            new Movimiento
                            {
                                Codigo = CodeGeneratorService.GenerateMovimientoCode(),
                                StockActual = stockDestino,
                                TipoMovimientoId = tipoMovId,
                                Cantidad = cantidadAMover,
                                EntidadReferenciaId = null,
                                Fecha = DateOnly.FromDateTime(DateTime.Now),
                                Estado = "Consolidación por Reubicación",
                            }
                        );
                    }
                }

                await context.SaveChangesAsync();
                await transaction.CommitAsync();

                return Ok(new { mensaje = "Transferencia de lotes completada con éxito." });
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest(
                    new
                    {
                        error = "Error al guardar cambios",
                        detalle = ex.InnerException?.Message ?? ex.Message,
                    }
                );
            }
        }
    }
}
