using Data;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiposUnidadesController : ControllerBase
    {
        private readonly AppDbContext context;

        public TiposUnidadesController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTipoUnidadMedida()
        {
            var tiposu = await (
                from tum in context.TiposUnidades
                where tum.Estado != "Eliminado"
                select tum
            ).ToListAsync();
            var tiposDto = tiposu.Select(t => t.ToReadDto()).ToList();
            return Ok(tiposDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostTipoUnidadMedida(string nombre, string abreviatura)
        {
            var tipoum = await (
                from tum in context.TiposUnidades
                where tum.Nombre == nombre
                select tum
            ).FirstOrDefaultAsync();
            if (tipoum != null)
            {
                return BadRequest("Ya existe esa Unidad de Medida");
            }
            var tipoUnidadMedida = new TipoUnidadMedida
            {
                Nombre = nombre,
                Abreviatura = abreviatura,
            };
            await context.TiposUnidades.AddAsync(tipoUnidadMedida);
            await context.SaveChangesAsync();

            return Ok("Se Agrego correctamente");
        }

        [HttpPut]
        public async Task<IActionResult> PutTipoUnidadMedida(string nombre, string Abreviatura)
        {
            var tipoum = await (
                from tum in context.TiposUnidades
                where tum.Nombre == nombre
                select tum
            ).FirstOrDefaultAsync();
            if (tipoum == null)
            {
                return BadRequest("No existe esa unidad de medida");
            }
            tipoum.Nombre = nombre;
            tipoum.Abreviatura = Abreviatura;
            await context.SaveChangesAsync();

            return Ok("Se modifico correctamente");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTipoUnidadMedida(string nombre)
        {
            var tipoum = await (
                from tum in context.TiposUnidades
                where tum.Nombre == nombre
                select tum
            ).FirstOrDefaultAsync();
            if (tipoum == null)
            {
                return BadRequest("No existe esa unidad de medida");
            }
            tipoum.Estado = "Eliminado";
            await context.SaveChangesAsync();

            return Ok("Se elimino correctamente");
        }
    }
}
