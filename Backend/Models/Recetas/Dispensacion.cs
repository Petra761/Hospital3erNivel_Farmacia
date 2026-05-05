using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("dispensacion")]
    public class Dispensacion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("codigo")]
        public string Codigo { get; set; }

        [Column("receta_id")]
        public int RecetaId { get; set; }

        [Column("farmaceutico_codigo")]
        public string FarmaceuticoCodigo { get; set; }

        [Column("quien_recoge")]
        public string? QuienRecoge { get; set; }

        [Column("fecha")]
        public DateOnly Fecha { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        // Relaciones
        [ForeignKey("RecetaId")]
        [JsonIgnore]
        public Receta Receta { get; set; }

        [JsonIgnore]
        public ICollection<DispensacionLote> DispensacionLotes { get; set; }
    }
}