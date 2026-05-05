namespace DTOs
{
    public record PosologiaReadDto(
        string Dosis,
        string ViaAdministracion,
        string Frecuencia,
        string Duracion,
        string IndicacionesAdicionales
    );

    public record PosologiaPostDto(
        decimal Dosis,
        string UnidadAbreviatura,
        string ViaAdministracion,
        string Frecuencia,
        int FrecuenciaValor,
        string Duracion,
        string IndicacionesAdicionales
    );
}
