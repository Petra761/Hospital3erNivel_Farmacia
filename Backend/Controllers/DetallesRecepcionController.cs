using Data;
using DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DetallesRecepcionController : ControllerBase
    {
        private readonly AppDbContext context;

        public DetallesRecepcionController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var detalles = await (
                from dr in context.DetallesRecepcion
                where dr.Estado != "Eliminado"
                select dr
            )
                .Include(dr => dr.Recepcion)
                .Include(dr => dr.Medicamento)
                    .ThenInclude(m => m.TipoMedicamento)
                .ToListAsync();

            var detallesDto = detalles.Select(d => d.ToReadDto()).ToList();

            return Ok(detallesDto);
        }
    }
}
