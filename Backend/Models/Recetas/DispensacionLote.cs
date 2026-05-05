using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("dispensacion_lote")]
    public class DispensacionLote
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("dispensacion_id")]
        public int DispensacionId { get; set; }

        [Column("stock_actual_id")]
        public int StockActualId { get; set; }

        [Column("detalle_receta_id")]
        public int DetalleRecetaId { get; set; }

        [Column("cantidad_entregada")]
        public int CantidadEntregada { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        // Relaciones Padres
        [ForeignKey("DispensacionId")]
        [JsonIgnore]
        public Dispensacion Dispensacion { get; set; }

        [ForeignKey("StockActualId")]
        [JsonIgnore]
        public StockActual StockActual { get; set; }

        [ForeignKey("DetalleRecetaId")]
        [JsonIgnore]
        public DetalleReceta DetalleReceta { get; set; }
    }
}