using Data;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiposMovimientosController : ControllerBase
    {
        private readonly AppDbContext context;

        public TiposMovimientosController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tipos = await (
                from tm in context.TiposMovimientos
                where tm.Estado != "Eliminado"
                select tm
            ).ToListAsync();
            var tiposDto = tipos.Select(t => t.ToDto()).ToList();
            return Ok(tiposDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostTipoMovimiento(string descripcion, bool esSuma)
        {
            var tipom = await (
                from tm in context.TiposMovimientos
                where tm.Estado != "Eliminado" && tm.Descripcion == descripcion
                select tm
            ).FirstOrDefaultAsync();
            if (tipom != null)
                return BadRequest("Ya existe este tipo de movimiento");

            var tipoMovimiento = new TipoMovimiento
            {
                Descripcion = descripcion,
                EsSuma = esSuma,
                Estado = "Activo",
            };

            await context.TiposMovimientos.AddAsync(tipoMovimiento);
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{descripcion}")]
        public async Task<IActionResult> PutTipoMovimiento(string descripcion, string nDescripcion)
        {
            var tipom = await (
                from tm in context.TiposMovimientos
                where tm.Estado != "Eliminado" && tm.Descripcion == descripcion
                select tm
            ).FirstOrDefaultAsync();
            if (tipom == null)
                return NotFound("No se encontro un tipo movimiento con esa descripcion");

            tipom.Descripcion = nDescripcion;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{descripcion}")]
        public async Task<IActionResult> DeleteTipoMovimiento(string descripcion)
        {
            var tipom = await (
                from tm in context.TiposMovimientos
                where tm.Estado != "Eliminado" && tm.Descripcion == descripcion
                select tm
            ).FirstOrDefaultAsync();
            if (tipom == null)
                return NotFound("No se encontro un tipo movimiento con esa descripcion");

            tipom.Estado = "Eliminado";
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
