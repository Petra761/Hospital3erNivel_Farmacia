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

    public record RankingMedicamentoReadDto(
        string MedicamentoCodigo,
        string MedicamentoNombre,
        int TotalUnidadesSolicitadas,
        int CantidadRecetas,
        string PromedioPorReceta
    );

    public record KardexMedicamentoReadDto(
        string MedicamentoCodigo,
        string MedicamentoNombre,
        int StockTotalActual,
        List<MovimientoKardexDto> Historial
    );

    public record MovimientoKardexDto(
        string Fecha,
        string TipoMovimiento,
        string LoteCodigo,
        int Cantidad,
        string Signo,
        string ReferenciaCodigo
    );
}
