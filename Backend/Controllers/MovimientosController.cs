using Data;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [ApiController]
    [Route("api[controller]")]
    public class MovimientosController : ControllerBase
    {
        private readonly AppDbContext context;

        public MovimientosController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var movimientos = await (
                from m in context.Movimientos
                where m.Estado != "Eliminado"
                select m
            )
                .Include(m => m.StockActual)
                    .ThenInclude(m => m.Lote)
                .Include(m => m.TipoMovimiento)
                .ToListAsync();

            var movimientosDto = movimientos.Select(m => m.ToReadDto()).ToList();

            return Ok(movimientosDto);
        }
    }
}
