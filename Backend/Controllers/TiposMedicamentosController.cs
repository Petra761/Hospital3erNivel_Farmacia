using System.CodeDom.Compiler;
using System.ComponentModel.DataAnnotations.Schema;
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
    public class TiposMedicamentosController : ControllerBase
    {
        private readonly AppDbContext context;

        public TiposMedicamentosController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTiposMedicamentos()
        {
            var tiposmed = await (
                from tm in context.TiposMedicamentos
                where tm.Estado != "Eliminado"
                select tm
            ).ToListAsync();
            var tiposDto = tiposmed.Select(t => t.ToReadDto()).ToList();
            return Ok(tiposDto);
        }

        [HttpGet("{codigo}")]
        public async Task<IActionResult> GetTipoMedicamento(string codigo)
        {
            var tipom = await (
                from tm in context.TiposMedicamentos
                where tm.Estado != "Eliminado" && tm.Codigo == codigo
                select tm
            ).FirstOrDefaultAsync();
            if (tipom == null)
                return NotFound("No se encontro el Tipo de Medicamento");
            var tipodto = tipom.ToReadDto();
            return Ok(tipodto);
        }

        [HttpGet("{codigo}/detalle")]
        public async Task<IActionResult> GetTipoConDetalle(string codigo)
        {
            var tipo = await (
                from tm in context.TiposMedicamentos
                where tm.Estado != "Eliminado" && tm.Codigo == codigo
                select tm
            ).FirstOrDefaultAsync();
            if (tipo == null)
                return NotFound("No se encontro el Medicamento Base");

            var presentaciones = await (
                from m in context.Medicamentos
                join tum in context.TiposUnidades on m.UnidadMedidaId equals tum.Id
                join ff in context.FormasFarmaceuticas on m.FormaId equals ff.Id
                where m.MedicamentoId == tipo.Id && m.Estado != "Eliminado"
                select new MedicamentoReadDto(
                    m.Codigo,
                    tipo.NombreGenerico,
                    tipo.NombreComercial,
                    tum.Nombre,
                    ff.Nombre,
                    m.ValorConcentracion.ToString()
                )
            ).ToListAsync();

            var tipoDetalleDto = new TipoMedicamentoDetalleDto(
                tipo.Codigo,
                tipo.NombreGenerico,
                tipo.NombreComercial,
                tipo.EsControlado,
                tipo.RequiereRefrigeracion,
                presentaciones
            );

            return Ok(tipoDetalleDto);
        }

        [HttpPost]
        public async Task<IActionResult> PostTipoMedicamento(TipoMedicamentoPostDto dto)
        {
            var tipoMedicamento = new TipoMedicamento
            {
                Codigo = CodeGeneratorService.GenerateTipoMedicamentoCode(dto.NombreGenerico),
                NombreGenerico = dto.NombreGenerico,
                NombreComercial = dto.NombreComercial,
                EsControlado = dto.EsControlado,
                RequiereRefrigeracion = dto.RequiereRefrigeracion,
                StockMinimoAlerta = dto.StockMinimoAlerta,
                Estado = "Activo",
            };

            await context.TiposMedicamentos.AddAsync(tipoMedicamento);
            await context.SaveChangesAsync();

            return Ok("Se agrego correctamente");
        }

        [HttpPut("{codigo}")]
        public async Task<IActionResult> PutTipoMedicamento(
            string codigo,
            TipoMedicamentoPutDto nuevoTipo
        )
        {
            var tipoMed = await (
                from tm in context.TiposMedicamentos
                where tm.Estado != "Eliminado" && tm.Codigo == codigo
                select tm
            ).FirstOrDefaultAsync();
            if (tipoMed == null)
            {
                return BadRequest("No se encontro el Tipo de Medicamento");
            }

            tipoMed.NombreGenerico = nuevoTipo.NombreComercial;
            tipoMed.NombreComercial = nuevoTipo.NombreComercial;
            tipoMed.EsControlado = nuevoTipo.EsControlado;
            tipoMed.RequiereRefrigeracion = nuevoTipo.RequiereRefrigeracion;
            tipoMed.StockMinimoAlerta = nuevoTipo.StockMinimoAlerta;

            await context.SaveChangesAsync();

            return Ok("Se modifico Correctamente");
        }

        [HttpDelete("{codigo}")]
        public async Task<IActionResult> DeleteTipoMedicamento(string codigo)
        {
            var tipoMed = await (
                from tm in context.TiposMedicamentos
                where tm.Estado != "Eliminado" && tm.Codigo == codigo
                select tm
            ).FirstOrDefaultAsync();
            if (tipoMed == null)
            {
                return BadRequest("No se encontro el Tipo de Medicamento");
            }

            tipoMed.Estado = "Eliminado";

            await context.SaveChangesAsync();

            return Ok("Se elimino Correctamente");
        }
    }
}
