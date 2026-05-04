using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("detalle_recepcion")]
    public class DetalleRecepcion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("recepcion_id")]
        public int RecepcionId { get; set; }

        [Column("medicamento_id")]
        public int MedicamentoId { get; set; }

        [Column("cantidad_recibida")]
        public int CantidadRecibida { get; set; }

        [Column("fecha_vencimiento")]
        public DateOnly FechaVencimiento { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        [ForeignKey("RecepcionId")]
        [JsonIgnore]
        public Recepcion Recepcion { get; set; }

        [ForeignKey("MedicamentoId")]
        [JsonIgnore]
        public Medicamento Medicamento { get; set; }

        [JsonIgnore]
        public ICollection<Lote> Lotes { get; set; }
    }
}
