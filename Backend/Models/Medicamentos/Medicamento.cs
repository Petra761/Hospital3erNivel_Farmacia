using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("medicamentos")]
    public class Medicamento
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("codigo")]
        [Required]
        public string Codigo { get; set; }

        [Column("medicamento_id")]
        public int MedicamentoId { get; set; }

        [Column("unidad_medida_id")]
        public int UnidadMedidaId { get; set; }

        [Column("forma_id")]
        public int FormaId { get; set; }

        [Column("valor_concentracion")]
        public decimal ValorConcentracion { get; set; }

        [Column("estado")]
        public string Estado { get; set; } = "Activo";

        // Relaciones
        [ForeignKey("MedicamentoId")]
        public TipoMedicamento TipoMedicamento { get; set; }

        [ForeignKey("UnidadMedidaId")]
        public TipoUnidadMedida TipoUnidadMedida { get; set; }

        [ForeignKey("FormaId")]
        public FormaFarmaceutica FormaFarmaceutica { get; set; }
    }
}