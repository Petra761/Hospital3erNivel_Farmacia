using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("recepciones")]
    public class Recepcion
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("codigo")]
        public string Codigo { get; set; }

        [Column("fecha_recepcion")]
        public DateOnly FechaRecepcion { get; set; } 

        [Column("recibido_por_codigo")]
        public string RecibidoPorCodigo { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        [JsonIgnore]
        public ICollection<DetalleRecepcion> Detalles { get; set; }
    }
}