using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("lotes")]
    public class Lote
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("codigo")]
        public string Codigo { get; set; }

        [Column("medicamento_id")]
        public int MedicamentoId { get; set; }

        [Column("detalle_recepcion_id")]
        public int DetalleRecepcionId { get; set; }

        [Column("cantidad_inicial")]
        public int CantidadInicial { get; set; }

        [Column("fecha_ingreso")]
        public DateOnly FechaIngreso { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        [ForeignKey("MedicamentoId")]
        [JsonIgnore]
        public Medicamento Medicamento { get; set; }

        [ForeignKey("DetalleRecepcionId")]
        [JsonIgnore]
        public DetalleRecepcion DetalleRecepcion { get; set; }

        [JsonIgnore]
        public ICollection<StockActual> Stocks { get; set; }
    }
}