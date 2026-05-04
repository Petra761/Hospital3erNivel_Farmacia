using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("tipos_medicamentos")]
    public class TipoMedicamento
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("codigo")]
        [Required]
        public string Codigo { get; set; }

        [Column("nombre_generico")]
        public string NombreGenerico { get; set; }

        [Column("nombre_comercial")]
        public string NombreComercial { get; set; }

        [Column("es_controlado")]
        public bool EsControlado { get; set; }

        [Column("requiere_refrigeracion")]
        public bool RequiereRefrigeracion { get; set; }

        [Column("stock_minimo_alerta")]
        public int StockMinimoAlerta { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        [JsonIgnore]
        public ICollection<Medicamento> MedicamentosDetallados { get; set; }
    }
}