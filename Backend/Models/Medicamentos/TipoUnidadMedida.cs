using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
[Table("tipos_unidad_medida")]
    public class TipoUnidadMedida
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nombre")]
        [Required]
        public string Nombre { get; set; }

        [Column("abreviatura")]
        public string Abreviatura { get; set; }

        [Column("estado")]
        public string Estado { get; set; } = "Activo";

        [JsonIgnore]
        public ICollection<Medicamento> Medicamentos { get; set; }
    }
}