using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("movimientos")]
    public class Movimiento
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("codigo")]
        [Required]
        public string Codigo { get; set; }

        [Column("stock_actual_id")]
        public int StockActualId { get; set; }

        [Column("tipo_movimiento_id")]
        public int TipoMovimientoId { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("entidad_referencia_id")]
        public int? EntidadReferenciaId { get; set; }

        [Column("fecha")]
        public DateOnly Fecha { get; set; }

        [Column("observaciones")]
        public string? Observaciones { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        [ForeignKey("StockActualId")]
        [JsonIgnore]
        public StockActual StockActual { get; set; }

        [ForeignKey("TipoMovimientoId")]
        [JsonIgnore]
        public TipoMovimiento TipoMovimiento { get; set; }
    }
}
