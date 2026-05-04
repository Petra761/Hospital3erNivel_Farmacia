using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("ubicaciones_almacen")]
    public class UbicacionAlmacen
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("codigo")]
        [Required]
        public string Codigo { get; set; }

        [Column("nombre")]
        public string Nombre { get; set; }

        [Column("estado")]
        public string Estado { get; set; } 
        
        [JsonIgnore]
        public ICollection<StockActual> Stocks { get; set; }
    }
}