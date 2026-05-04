using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models
{
    [Table("posologia")]
    public class Posologia
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("codigo")]
        public string Codigo { get; set; }

        [Column("detalle_receta_id")]
        public int DetalleRecetaId { get; set; }

        [Column("dosis", TypeName = "decimal(18,2)")]
        public decimal Dosis { get; set; }

        [Column("unidad_id")]
        public int UnidadId { get; set; }

        [Column("via_administracion")]
        public string ViaAdministracion { get; set; }

        [Column("frecuencia")]
        public string Frecuencia { get; set; }

        [Column("frecuencia_valor")]
        public int FrecuenciaValor { get; set; }

        [Column("duracion")]
        public string Duracion { get; set; }

        [Column("indicaciones_adicionales")]
        public string IndicacionesAdicionales { get; set; }

        [Column("estado")]
        public string Estado { get; set; }

        // Relaciones
        [ForeignKey("DetalleRecetaId")]
        [JsonIgnore]
        public DetalleReceta DetalleReceta { get; set; }

        [ForeignKey("UnidadId")]
        [JsonIgnore]
        public TipoUnidadMedida TipoUnidadMedida { get; set; }
    }
}