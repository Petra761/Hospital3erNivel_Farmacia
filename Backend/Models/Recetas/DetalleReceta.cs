using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("detalle_receta")]
    public class DetalleReceta
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("receta_id")]
        public int RecetaId { get; set; }

        [Column("medicamento_id")]
        public int MedicamentoId { get; set; }

        [Column("cantidad_solicitada")]
        public int CantidadSolicitada { get; set; }

        [Column("estado")]
        public string Estado { get; set; } // entregado parcial, entregado completamente

        // Relaciones (Padres)
        [ForeignKey("RecetaId")]
        [JsonIgnore]
        public Receta Receta { get; set; }

        [ForeignKey("MedicamentoId")]
        [JsonIgnore]
        public Medicamento Medicamento { get; set; }

        // Relación 1:1 con Posologia
        [JsonIgnore]
        public Posologia Posologia { get; set; }
    }
}