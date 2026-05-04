using Data;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Services;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UbicacionesAlmacenController : ControllerBase
    {
        private readonly AppDbContext context;

        public UbicacionesAlmacenController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var Ubicaciones = await (
                from ua in context.UbicacionesAlmacen
                where ua.Estado != "Eliminado"
                select ua
            ).ToListAsync();
            var UbicacionesDto = Ubicaciones.Select(u => u.ToReadDto()).ToList();
            return Ok(UbicacionesDto);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetUbicacion(string codigo)
        {
            var Ubicacion = await (
                from ua in context.UbicacionesAlmacen
                where ua.Estado != "Eliminado" && ua.Codigo == codigo
                select ua
            ).FirstOrDefaultAsync();
            if (Ubicacion == null)
                return NotFound("No se encontro la ubicacion");
            return Ok(Ubicacion.ToReadDto());
        }

        [HttpPost]
        public async Task<IActionResult> Postubicacion(UbicacionAlmacenPostDto nUa)
        {
            var yaExiste = await (
                from ua in context.UbicacionesAlmacen
                where ua.Estado != "Eliminado" && ua.Nombre == nUa.Nombre
                select ua
            ).FirstOrDefaultAsync();
            if (yaExiste != null)
                return BadRequest("Ya existe esta ubicacion");

            var ubicaiconAlmacen = new UbicacionAlmacen
            {
                Codigo = CodeGeneratorService.GenerateUbicacionCode(nUa.Nombre),
                Nombre = nUa.Nombre,
                Estado = "Activo",
            };

            await context.UbicacionesAlmacen.AddAsync(ubicaiconAlmacen);
            await context.SaveChangesAsync();

            return Ok($"Se creo con el codigo {ubicaiconAlmacen.Codigo}");
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutUbicaion(string codigo, string nombre)
        {
            var ubicacion = await (
                from ua in context.UbicacionesAlmacen
                where ua.Estado != "Eliminado" && ua.Codigo == codigo
                select ua
            ).FirstOrDefaultAsync();
            if (ubicacion == null)
                return NotFound("No se encontro la ubicacion con ese codigo");

            ubicacion.Nombre = nombre;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteUbicaion(string codigo)
        {
            var ubicacion = await (
                from ua in context.UbicacionesAlmacen
                where ua.Estado != "Eliminado" && ua.Codigo == codigo
                select ua
            ).FirstOrDefaultAsync();
            if (ubicacion == null)
                return NotFound("No se encontro la ubicacion con ese codigo");

            ubicacion.Estado = "Eliminado";
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
