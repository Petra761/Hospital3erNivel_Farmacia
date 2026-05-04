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
    public class RecepcionesController : ControllerBase
    {
        private readonly AppDbContext context;

        public RecepcionesController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var recepciones = await (
                from r in context.Recepciones
                where r.Estado != "Eliminado"
                select r
            ).ToListAsync();

            var recepcionesDto = recepciones.Select(r => r.ToReadDto()).ToList();

            return Ok(recepcionesDto);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetRecepcion(string codigo)
        {
            var recepcion = (
                from r in context.Recepciones
                where r.Estado != "Elimnado" && r.Codigo == codigo
                select r
            ).FirstOrDefaultAsync();

            if (recepcion == null)
                return NotFound("No se encontro la Recepcion");

            return Ok(recepcion);
        }

        [HttpPost]
        public async Task<IActionResult> PostRecepcion(RecepcionPostDto r)
        {
            var codigos = r.Detalles.Select(c => c.MedicamentoCodigo).Distinct().ToList();

            var medicamentos = await (
                from m in context.Medicamentos
                where codigos.Contains(m.Codigo)
                select m
            ).ToDictionaryAsync(m => m.Codigo, m => m.Id);

            var invalidos = codigos.Where(c => !medicamentos.ContainsKey(c)).ToList();

            if (invalidos.Any())
                return BadRequest($"Códigos inválidos: {string.Join(", ", invalidos)}");

            var recepcion = new Recepcion
            {
                Codigo = CodeGeneratorService.GenerateRecepcionCode(),
                FechaRecepcion = DateOnly.FromDateTime(DateTime.Now),
                RecibidoPorCodigo = r.RecibidoPorCodigo,
                Estado = r.Estado,
                Detalles = r
                    .Detalles.Select(item => new DetalleRecepcion
                    {
                        MedicamentoId = medicamentos[item.MedicamentoCodigo],
                        CantidadRecibida = item.CantidadRecibida,
                        Estado = item.Estado,
                        FechaVencimiento = item.FechaVencimiento,
                    })
                    .ToList(),
            };

            try
            {
                context.Recepciones.Add(recepcion);
                await context.SaveChangesAsync();

                return Ok($"Se creo la recepción con código {recepcion.Codigo}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
