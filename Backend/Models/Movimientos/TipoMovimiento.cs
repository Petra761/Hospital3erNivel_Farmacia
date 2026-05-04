using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("tipos_movimiento")]
    public class TipoMovimiento
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("descripcion")]
        [Required]
        public string Descripcion { get; set; }

        [Column("es_suma")]
        public bool EsSuma { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        [JsonIgnore]
        public ICollection<Movimiento> Movimientos { get; set; }
    }
}
