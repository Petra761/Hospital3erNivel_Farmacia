using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("formas_farmaceuticas")]
    public class FormaFarmaceutica
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nombre")]
        [Required]
        public string Nombre { get; set; }

        [Column("estado")]
        public string Estado { get; set; } = "Activo";

        [JsonIgnore]
        public ICollection<Medicamento> Medicamentos { get; set; }
    }
}