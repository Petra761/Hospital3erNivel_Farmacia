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
    public class RecetasController : ControllerBase
    {
        private readonly AppDbContext context;

        public RecetasController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var recetas = await (
                from r in context.Recetas
                where r.Estado != "Eliminado"
                orderby r.FechaSolicitud descending
                select new RecetaReadDto(
                    r.Codigo,
                    r.PacienteCodigo,
                    "Paciente Wilson Comsume API", //Pendiente la integracion de paciente y dr
                    r.MedicoCodigo,
                    "Dr Herberth",
                    r.FechaSolicitud,
                    r.Estado,
                    (
                        from d in context.DetallesReceta
                        join m in context.Medicamentos on d.MedicamentoId equals m.Id
                        join tm in context.TiposMedicamentos on m.MedicamentoId equals tm.Id
                        join p in context.Posologias on d.Id equals p.DetalleRecetaId
                        where d.RecetaId == r.Id
                        select new DetalleRecetaReadDto(
                            m.Codigo,
                            tm.NombreGenerico + " (" + tm.NombreComercial + ")",
                            d.CantidadSolicitada,
                            d.Estado,
                            new PosologiaReadDto(
                                p.Dosis.ToString("N2") + " " + p.UnidadMedida,
                                p.ViaAdministracion,
                                "Cada " + p.FrecuenciaValor + " " + p.Frecuencia,
                                p.Duracion,
                                p.IndicacionesAdicionales ?? "Sin indicaciones extra"
                            )
                        )
                    ).ToList()
                )
            ).ToListAsync();

            return Ok(recetas);
        }

        [HttpGet("seguimiento/{codigo}")]
        public async Task<IActionResult> GetRecetaSeguimiento(string codigo)
        {
            var seguimiento = await (
                from r in context.Recetas
                where r.Estado != "Eliminado" && r.Codigo == codigo
                select new SeguimientoRecetaReadDto(
                    r.Codigo,
                    "Paciente Wilson consume API",
                    "Dr Herberth",
                    r.Estado,
                    (
                        from d in context.DetallesReceta
                        join m in context.Medicamentos on d.MedicamentoId equals m.Id
                        join tm in context.TiposMedicamentos on m.MedicamentoId equals tm.Id
                        where d.RecetaId == r.Id
                        let totalEntregado = (
                            from dl in context.DispensacionesLote
                            where dl.DetalleRecetaId == d.Id
                            select dl.CantidadEntregada
                        ).Sum()
                        select new SeguimientoItemReadDto(
                            tm.NombreGenerico + " (" + tm.NombreComercial + ")",
                            d.CantidadSolicitada,
                            totalEntregado,
                            d.CantidadSolicitada - totalEntregado,
                            d.Estado,
                            (
                                from dl in context.DispensacionesLote
                                join disp in context.Dispensaciones
                                    on dl.DispensacionId equals disp.Id
                                join sa in context.StocksActuales on dl.StockActualId equals sa.Id
                                join lot in context.Lotes on sa.LoteId equals lot.Id
                                where dl.DetalleRecetaId == d.Id
                                select new EntregaFisicaDto(
                                    disp.Fecha,
                                    lot.Codigo,
                                    dl.CantidadEntregada,
                                    disp.FarmaceuticoCodigo
                                )
                            ).ToList()
                        )
                    ).ToList()
                )
            ).FirstOrDefaultAsync();

            if (seguimiento == null)
                return NotFound(new { message = $"La receta con código {codigo} no existe." });

            return Ok(seguimiento);
        }

        [HttpGet("receta/pendientes")]
        public async Task<IActionResult> GetRecetasPendientes()
        {
            var recetas = await (
                from r in context.Recetas
                where r.Estado != "Entregado" && r.Estado != "Eliminado"
                orderby r.FechaSolicitud descending
                select new RecetaReadDto(
                    r.Codigo,
                    r.PacienteCodigo,
                    "Paciente Wilson consume API",
                    r.MedicoCodigo,
                    "Dr. Herberth",
                    r.FechaSolicitud,
                    r.Estado,
                    (
                        from d in context.DetallesReceta
                        join m in context.Medicamentos on d.MedicamentoId equals m.Id
                        join tm in context.TiposMedicamentos on m.MedicamentoId equals tm.Id
                        join p in context.Posologias on d.Id equals p.DetalleRecetaId
                        where d.RecetaId == r.Id && d.Estado != "Entregado Total"
                        select new DetalleRecetaReadDto(
                            m.Codigo,
                            tm.NombreGenerico + " (" + tm.NombreComercial + ")",
                            d.CantidadSolicitada,
                            d.Estado,
                            new PosologiaReadDto(
                                p.Dosis.ToString("N2") + " " + p.UnidadMedida,
                                p.ViaAdministracion,
                                "Cada " + p.FrecuenciaValor + " " + p.Frecuencia,
                                p.Duracion,
                                p.IndicacionesAdicionales ?? "Sin indicaciones extras"
                            )
                        )
                    ).ToList()
                )
            ).ToListAsync();

            return Ok(recetas);
        }

        [HttpGet("receta/{codigoPaciente}")]
        public async Task<IActionResult> GetRecetasPorPaciente(string codigoPaciente)
        {
            var recetas = await (
                from r in context.Recetas
                where r.PacienteCodigo == codigoPaciente && r.Estado != "Eliminado"
                orderby r.FechaSolicitud descending
                select new RecetaReadDto(
                    r.Codigo,
                    r.PacienteCodigo,
                    "Paciente Wilson consume API", // Temporal
                    r.MedicoCodigo,
                    "Dr. Herberth", // Temporal
                    r.FechaSolicitud,
                    r.Estado,
                    (
                        from d in context.DetallesReceta
                        join m in context.Medicamentos on d.MedicamentoId equals m.Id
                        join tm in context.TiposMedicamentos on m.MedicamentoId equals tm.Id
                        join p in context.Posologias on d.Id equals p.DetalleRecetaId
                        where d.RecetaId == r.Id
                        select new DetalleRecetaReadDto(
                            m.Codigo,
                            tm.NombreGenerico + " (" + tm.NombreComercial + ")",
                            d.CantidadSolicitada,
                            d.Estado,
                            new PosologiaReadDto(
                                p.Dosis.ToString("N2") + " " + p.UnidadMedida,
                                p.ViaAdministracion,
                                "Cada " + p.FrecuenciaValor + " " + p.Frecuencia,
                                p.Duracion,
                                p.IndicacionesAdicionales ?? "Sin indicaciones extras"
                            )
                        )
                    ).ToList()
                )
            ).ToListAsync();

            return Ok(recetas);
        }

        [HttpGet("receta/{codigoPaciente}/pendientes")]
        public async Task<IActionResult> GetRecetasPendientesPorPaciente(string codigoPaciente)
        {
            var recetas = await (
                from r in context.Recetas
                where
                    r.PacienteCodigo == codigoPaciente
                    && r.Estado != "Entregado"
                    && r.Estado != "Eliminado"
                orderby r.FechaSolicitud descending
                select new RecetaReadDto(
                    r.Codigo,
                    r.PacienteCodigo,
                    "Paciente Wilson consume API",
                    r.MedicoCodigo,
                    "Dr. Herberth",
                    r.FechaSolicitud,
                    r.Estado,
                    (
                        from d in context.DetallesReceta
                        join m in context.Medicamentos on d.MedicamentoId equals m.Id
                        join tm in context.TiposMedicamentos on m.MedicamentoId equals tm.Id
                        join p in context.Posologias on d.Id equals p.DetalleRecetaId
                        where d.RecetaId == r.Id && d.Estado != "Entregado Total"
                        select new DetalleRecetaReadDto(
                            m.Codigo,
                            tm.NombreGenerico + " (" + tm.NombreComercial + ")",
                            d.CantidadSolicitada,
                            d.Estado,
                            new PosologiaReadDto(
                                p.Dosis.ToString("N2") + " " + p.UnidadMedida,
                                p.ViaAdministracion,
                                "Cada " + p.FrecuenciaValor + " " + p.Frecuencia,
                                p.Duracion,
                                p.IndicacionesAdicionales ?? "Sin indicaciones extras"
                            )
                        )
                    ).ToList()
                )
            ).ToListAsync();

            return Ok(recetas);
        }

        [HttpPost]
        public async Task<IActionResult> PostReceta(RecetaPostDto receta)
        {
            var codigosMed = receta.Detalles.Select(d => d.MedicamentoCodigo).ToList();

            var medicamentos = await (
                from m in context.Medicamentos
                where codigosMed.Contains(m.Codigo)
                select m
            ).ToDictionaryAsync(m => m.Codigo, m => m.Id);

            var noEncontrado = codigosMed.Where(c => !medicamentos.ContainsKey(c)).ToList();
            if (noEncontrado.Any())
                return BadRequest(
                    $"Los siguientes medicamentos no existen en el catálogo: {string.Join(", ", noEncontrado)}"
                );

            var nReceta = new Receta
            {
                Codigo = CodeGeneratorService.GenerateRecetaCode(),
                PacienteCodigo = receta.PacienteCodigo,
                MedicoCodigo = receta.MedicoCodigo,
                FechaSolicitud = DateOnly.FromDateTime(DateTime.Now),
                Estado = "Pendiente",
                Detalles = receta
                    .Detalles.Select(item => new DetalleReceta
                    {
                        MedicamentoId = medicamentos[item.MedicamentoCodigo],
                        CantidadSolicitada = item.CantidadSolicitada,
                        Estado = "Activo",
                        Posologia = new Posologia
                        {
                            Codigo = CodeGeneratorService.GeneratePosologiaCode(),
                            Dosis = item.Posologia.Dosis,
                            UnidadMedida = item.Posologia.UnidadAbreviatura,
                            ViaAdministracion = item.Posologia.ViaAdministracion,
                            Frecuencia = item.Posologia.Frecuencia,
                            FrecuenciaValor = item.Posologia.FrecuenciaValor,
                            Duracion = item.Posologia.Duracion,
                            IndicacionesAdicionales = item.Posologia.IndicacionesAdicionales,
                            Estado = "Activo",
                        },
                    })
                    .ToList(),
            };

            try
            {
                context.Recetas.Add(nReceta);
                await context.SaveChangesAsync();

                return Ok($"Se creo la Receta con código {nReceta.Codigo}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
    }
}
