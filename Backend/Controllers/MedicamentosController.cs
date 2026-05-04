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
    public class MedicamentosController : ControllerBase
    {
        private readonly AppDbContext context;

        public MedicamentosController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicamentos()
        {
            var medicamentos = await (
                from m in context.Medicamentos
                join tm in context.TiposMedicamentos on m.MedicamentoId equals tm.Id
                join tum in context.TiposUnidades on m.UnidadMedidaId equals tum.Id
                join ff in context.FormasFarmaceuticas on m.FormaId equals ff.Id
                where m.Estado != "Eliminado"
                select m
            )
                .Include(m => m.TipoMedicamento)
                .Include(m => m.TipoUnidadMedida)
                .Include(m => m.FormaFarmaceutica)
                .ToListAsync();

            var medicamentosDto = medicamentos.Select(m => m.ToReadDto());

            return Ok(medicamentosDto);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetMedicamento(string codigo)
        {
            var medicamento = await (
                from m in context.Medicamentos
                join tm in context.TiposMedicamentos on m.MedicamentoId equals tm.Id
                join tum in context.TiposUnidades on m.UnidadMedidaId equals tum.Id
                join ff in context.FormasFarmaceuticas on m.FormaId equals ff.Id
                where m.Estado != "Eliminado" && m.Codigo == codigo
                select m
            )
                .Include(m => m.TipoMedicamento)
                .Include(m => m.TipoUnidadMedida)
                .Include(m => m.FormaFarmaceutica)
                .FirstOrDefaultAsync();

            if (medicamento == null)
                return NotFound($"No se encontro el medicamento con el codigo {codigo}");

            var medicamentoDto = medicamento.ToReadDto();

            return Ok(medicamento);
        }

        [HttpPost]
        public async Task<IActionResult> PostMedicamento(MedicamentoPostDto med)
        {
            var tipoMed = await (
                from tm in context.TiposMedicamentos
                where tm.Estado != "Eliminado" && tm.Codigo == med.TipoMedicamentoCodigo
                select tm
            ).FirstOrDefaultAsync();

            var unidad = await (
                from um in context.TiposUnidades
                where um.Estado != "Eliminado" && um.Nombre == med.UnidadMedidaNombre
                select um
            ).FirstOrDefaultAsync();

            var forma = await (
                from f in context.FormasFarmaceuticas
                where f.Estado != "Eliminado" && f.Nombre == med.FormaFarmaceuticaNombre
                select f
            ).FirstOrDefaultAsync();

            if (tipoMed == null)
                return BadRequest(
                    $"No existe una unidad de medida con este codigo {med.TipoMedicamentoCodigo}"
                );
            if (unidad == null)
                return BadRequest($"No existe una unidad de medida {med.UnidadMedidaNombre}");
            if (forma == null)
                return BadRequest(
                    $"No existe ninguna forma de medida {med.FormaFarmaceuticaNombre}"
                );

            var yaExite = await (
                from me in context.Medicamentos
                where
                    me.MedicamentoId == tipoMed.Id
                    && me.UnidadMedidaId == unidad.Id
                    && me.FormaId == forma.Id
                    && me.Estado != "Eliminado"
                select me
            ).FirstOrDefaultAsync();

            if (yaExite != null)
                return BadRequest(
                    "Ya existe esta conbinacion de Medicamento Unida de medida y Forma"
                );

            var medicamento = new Medicamento
            {
                Codigo = CodeGeneratorService.GenerateMedicamentoCode(
                    tipoMed.NombreComercial,
                    forma.Nombre
                ),
                MedicamentoId = tipoMed.Id,
                UnidadMedidaId = unidad.Id,
                FormaId = forma.Id,
                ValorConcentracion = med.ValorConcentracion,
                Estado = "Activo",
            };

            await context.Medicamentos.AddAsync(medicamento);
            await context.SaveChangesAsync();

            return Ok($"Se creo exitosamente con el codigo {medicamento.Codigo}");
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutMedicamento(string codigo, MedicamentoPutDto nMed)
        {
            var med = await (
                from m in context.Medicamentos
                where m.Estado != "Eliminado" && m.Codigo == codigo
                select m
            ).FirstOrDefaultAsync();
            if (med == null)
            {
                return NotFound($"No se encontro el medicamento con el codigo {codigo}");
            }

            var unidad = await (
                from um in context.TiposUnidades
                where um.Estado != "Eliminado" && um.Nombre == nMed.UnidadMedidaNombre
                select um
            ).FirstOrDefaultAsync();
            var forma = await (
                from f in context.FormasFarmaceuticas
                where f.Estado != "Eliminado" && f.Nombre == nMed.FormaFarmaceuticaNombre
                select f
            ).FirstOrDefaultAsync();

            if (unidad == null || forma == null)
                return BadRequest(
                    "La unidad de medida o forma farmaceutica no se encontro o esta inactiva"
                );

            var yaExite = await (
                from me in context.Medicamentos
                where
                    me.MedicamentoId == med.Id
                    && me.UnidadMedidaId == unidad.Id
                    && me.FormaId == forma.Id
                    && me.Estado != "Eliminado"
                select me
            ).FirstOrDefaultAsync();

            if (yaExite != null)
                return BadRequest(
                    "Ya existe esta conbinacion de Medicamento Unida de medida y Forma"
                );

            med.UnidadMedidaId = unidad.Id;
            med.FormaId = forma.Id;
            med.ValorConcentracion = nMed.ValorConcentracion;

            await context.SaveChangesAsync();

            return Ok("Se modifico correctamente");
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteMedicamento(string codigo)
        {
            var med = await (
                from m in context.Medicamentos
                where m.Estado != "Eliminado" && m.Codigo == codigo
                select m
            ).FirstOrDefaultAsync();
            if (med == null)
            {
                return NotFound($"No se encontro el medicamento con el codigo {codigo}");
            }

            med.Estado = "Eliminado";

            await context.SaveChangesAsync();

            return Ok("Se elimino correctamente");
        }
    }
}
