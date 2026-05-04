using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("stock_actual")]
    public class StockActual
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("lote_id")]
        public int LoteId { get; set; }

        [Column("ubicacion_id")]
        public int UbicacionId { get; set; }

        [Column("cantidad")]
        public int Cantidad { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        [ForeignKey("LoteId")]
        [JsonIgnore]
        public Lote Lote { get; set; }

        [ForeignKey("UbicacionId")]
        [JsonIgnore]
        public UbicacionAlmacen Ubicacion { get; set; }

        [JsonIgnore]
        public ICollection<Movimiento> Movimientos { get; set; }
    }
}