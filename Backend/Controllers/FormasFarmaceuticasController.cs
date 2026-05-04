using Data;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormasFarmaceuticasController : ControllerBase
    {
        private readonly AppDbContext context;

        public FormasFarmaceuticasController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormasFarmaceuticas()
        {
            var formasf = await (
                from ffs in context.FormasFarmaceuticas
                where ffs.Estado != "Eliminado"
                select ffs
            ).ToListAsync();
            var formasDto = formasf.Select(f => f.ToReadDto()).ToList();
            return Ok(formasDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostFormaFarmaceutica(string nombre)
        {
            var formaf = await (
                from ffs in context.FormasFarmaceuticas
                where ffs.Nombre == nombre
                select ffs
            ).FirstOrDefaultAsync();
            if (formaf != null)
            {
                return BadRequest("Ya existe esa Forma Farmaceutica");
            }
            var formafarmaceutica = new FormaFarmaceutica { Nombre = nombre, Estado = "Activo" };
            await context.FormasFarmaceuticas.AddAsync(formafarmaceutica);
            await context.SaveChangesAsync();

            return Ok("Se guardo correctamente");
        }

        [HttpPut]
        public async Task<IActionResult> PutFormaFarmaceutica(string nombre)
        {
            var formaf = await (
                from ffs in context.FormasFarmaceuticas
                where ffs.Nombre == nombre
                select ffs
            ).FirstOrDefaultAsync();
            if (formaf == null)
            {
                return BadRequest("No se encontro la Forma Farmaceutica");
            }

            formaf.Nombre = nombre;
            await context.SaveChangesAsync();

            return Ok("Se modifico correctamente");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormaFarmaceutica(string nombre)
        {
            var formaf = await (
                from ffs in context.FormasFarmaceuticas
                where ffs.Nombre == nombre
                select ffs
            ).FirstOrDefaultAsync();
            if (formaf == null)
            {
                return BadRequest("No se encontro la Forma Farmaceutica");
            }

            formaf.Estado = "Eliminado";
            await context.SaveChangesAsync();

            return Ok("Se elimino correctamente correctamente");
        }
    }
}
