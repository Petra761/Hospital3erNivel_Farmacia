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
    }
}
