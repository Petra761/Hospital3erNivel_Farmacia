using Data;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("api[controller]")]
    public class StocksActualesController : ControllerBase
    {
        private readonly AppDbContext context;

        public StocksActualesController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await (
                from s in context.StocksActuales
                join l in context.Lotes on s.LoteId equals l.Id
                join dr in context.DetallesRecepcion on l.DetalleRecepcionId equals dr.Id
                join u in context.UbicacionesAlmacen on s.UbicacionId equals u.Id
                join m in context.Medicamentos on l.MedicamentoId equals m.Id
                join tm in context.TiposMedicamentos on m.MedicamentoId equals tm.Id
                join tu in context.TiposUnidades on m.UnidadMedidaId equals tu.Id
                join ff in context.FormasFarmaceuticas on m.FormaId equals ff.Id
                where s.Cantidad > 0
                select s
            )
                .Include(s => s.Ubicacion)
                .Include(s => s.Lote)
                    .ThenInclude(l => l.DetalleRecepcion)
                .Include(s => s.Lote)
                    .ThenInclude(l => l.Medicamento)
                        .ThenInclude(m => m.TipoMedicamento)
                .Include(s => s.Lote)
                    .ThenInclude(l => l.Medicamento)
                        .ThenInclude(m => m.TipoUnidadMedida)
                .Include(s => s.Lote)
                    .ThenInclude(l => l.Medicamento)
                        .ThenInclude(m => m.FormaFarmaceutica)
                .ToListAsync();

            var stockDto = stocks.Select(s => s.ToReadDto());

            return Ok(stockDto);
        }

        [HttpGet("bajo-stock")]
        public async Task<IActionResult> GetBajoStock()
        {
            var reporteBajoStock = await (
                from m in context.Medicamentos
                join tm in context.TiposMedicamentos on m.MedicamentoId equals tm.Id
                where tm.Estado != "Eliminado"

                let stockTotal = (
                    from l in context.Lotes
                    join s in context.StocksActuales on l.Id equals s.LoteId
                    where l.MedicamentoId == m.Id
                    select s.Cantidad
                ).Sum()

                where stockTotal <= tm.StockMinimoAlerta

                select new BajoStockReadDto(
                    m.Codigo,
                    tm.NombreGenerico + " (" + tm.NombreComercial + ")",
                    stockTotal,
                    tm.StockMinimoAlerta,
                    tm.StockMinimoAlerta - stockTotal,
                    stockTotal == 0 ? "CRÍTICO (Agotado)" : "BAJO (Reponer)"
                )
            ).ToListAsync();

            return Ok(reporteBajoStock);
        }

        [HttpGet("disponibilidad/{codigoMedicamento}")]
        public async Task<IActionResult> GetStockPorMedicamento(string codigoMedicamento)
        {
            var stockInfo = await (
                from m in context.Medicamentos
                join tm in context.TiposMedicamentos on m.MedicamentoId equals tm.Id
                join ff in context.FormasFarmaceuticas on m.FormaId equals ff.Id
                join tu in context.TiposUnidades on m.UnidadMedidaId equals tu.Id
                where m.Codigo == codigoMedicamento && m.Estado != "Eliminado"

                let cantidadTotal = (
                    from l in context.Lotes
                    join s in context.StocksActuales on l.Id equals s.LoteId
                    where l.MedicamentoId == m.Id
                    select s.Cantidad
                ).Sum()

                select new StockPorMedicamentoReadDto(
                    m.Codigo,
                    tm.NombreGenerico + " (" + tm.NombreComercial + ")",
                    m.ValorConcentracion.ToString("N2") + " " + tu.Abreviatura,
                    ff.Nombre,
                    cantidadTotal,
                    tu.Nombre,
                    cantidadTotal == 0
                        ? "AGOTADO"
                        : (cantidadTotal <= tm.StockMinimoAlerta ? "STOCK BAJO" : "DISPONIBLE")
                )
            ).FirstOrDefaultAsync();

            if (stockInfo == null)
                return NotFound(
                    new
                    {
                        mensaje = $"El medicamento con código {codigoMedicamento} no existe o fue eliminado.",
                    }
                );

            return Ok(stockInfo);
        }

        [HttpGet("por-ubicacion/{codigoUbicacion}")]
        public async Task<IActionResult> GetStockPorUbicacion(string codigoUbicacion)
        {
            var reporte = await (
                from u in context.UbicacionesAlmacen
                where u.Codigo == codigoUbicacion && u.Estado != "Eliminado"
                select new ReporteStockUbicacionDto(
                    u.Codigo,
                    u.Nombre,
                    (
                        from s in context.StocksActuales
                        join l in context.Lotes on s.LoteId equals l.Id
                        join dr in context.DetallesRecepcion on l.DetalleRecepcionId equals dr.Id
                        join m in context.Medicamentos on l.MedicamentoId equals m.Id
                        join tm in context.TiposMedicamentos on m.MedicamentoId equals tm.Id
                        join ff in context.FormasFarmaceuticas on m.FormaId equals ff.Id
                        join tu in context.TiposUnidades on m.UnidadMedidaId equals tu.Id
                        where s.UbicacionId == u.Id && s.Cantidad > 0
                        orderby dr.FechaVencimiento ascending
                        select new ItemStockUbicacionDto(
                            tm.NombreGenerico + " (" + tm.NombreComercial + ")",
                            l.Codigo,
                            s.Cantidad,
                            dr.FechaVencimiento,
                            m.ValorConcentracion.ToString("N2")
                                + " "
                                + tu.Abreviatura
                                + " - "
                                + ff.Nombre,
                            l.Estado
                        )
                    ).ToList()
                )
            ).FirstOrDefaultAsync();

            if (reporte == null)
                return NotFound(
                    new { mensaje = $"No se encontró la ubicación con código {codigoUbicacion}" }
                );

            return Ok(reporte);
        }
    }
}
