using Microsoft.EntityFrameworkCore;
using Models;

namespace Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // --- GRUPO 1: CATÁLOGOS Y MEDICAMENTOS ---
        public DbSet<TipoUnidadMedida> TiposUnidades { get; set; }
        public DbSet<FormaFarmaceutica> FormasFarmaceuticas { get; set; }
        public DbSet<TipoMedicamento> TiposMedicamentos { get; set; }
        public DbSet<Medicamento> Medicamentos { get; set; }

        // --- GRUPO 2: INFRAESTRUCTURA ---
        public DbSet<UbicacionAlmacen> UbicacionesAlmacen { get; set; }

        // --- GRUPO 3: RECEPCIÓN (Logística) ---
        public DbSet<Recepcion> Recepciones { get; set; }
        public DbSet<DetalleRecepcion> DetallesRecepcion { get; set; }

        // --- GRUPO 4: INVENTARIO Y LOTES ---
        public DbSet<Lote> Lotes { get; set; }
        public DbSet<StockActual> StocksActuales { get; set; }

        // --- GRUPO 5: RECETAS Y POSOLOGÍA (Demanda Médica) ---
        /* Se agregarán: Receta, DetalleReceta, Posologia */

        // --- GRUPO 6: DISPENSACIÓN (Salidas de Farmacia) ---
        /* Se agregarán: Dispensacion, DispensacionLote */

        // --- GRUPO 7: AUDITORÍA (Kardex) ---
        public DbSet<TipoMovimiento> TiposMovimientos { get; set; }
        public DbSet<Movimiento> Movimientos { get; set; }
    }
}
