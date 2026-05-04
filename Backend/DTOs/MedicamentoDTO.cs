namespace DTOs
{
    public record MedicamentoReadDto(
        string Codigo,
        string NombreGenerico,
        string NombreComercial,
        string UnidadMedida,
        string FormaNombre,
        string ValorConcentracion
    );

    public record MedicamentoPostDto(
        string TipoMedicamentoCodigo,
        string UnidadMedidaNombre,
        string FormaFarmaceuticaNombre,
        decimal ValorConcentracion
    );

    public record MedicamentoPutDto(
        string UnidadMedidaNombre,
        string FormaFarmaceuticaNombre,
        decimal ValorConcentracion
    );
}
