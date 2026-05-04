using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("recetas")]
    public class Receta
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("codigo")]
        [Required]
        public string Codigo { get; set; }

        [Column("paciente_codigo")]
        public string PacienteCodigo { get; set; }

        [Column("medico_codigo")]
        public string MedicoCodigo { get; set; }

        [Column("fecha_solicitud")]
        public DateOnly FechaSolicitud { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        [JsonIgnore]
        public ICollection<DetalleReceta> Detalles { get; set; }
    }
}