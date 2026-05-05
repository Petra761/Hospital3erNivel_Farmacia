namespace DTOs
{
    public record LoteReadDto(
        string LoteCodigoInterno,
        string MedicamentoNombre,
        DateOnly FechaIngreso,
        DateOnly FechaVencimiento,
        int CantidadInicial,
        int StockActualTotal,
        int DiasParaVencer,
        string Estado
    );

    public record LoteVencimientoReadDto(
        string LoteCodigo,
        string MedicamentoNombre,
        string UbicacionNombre,
        int CantidadActual,
        DateOnly FechaVencimiento,
        int DiasRestantes,
        string Prioridad
    );

    namespace DTOs
    {
        public record MoverLotesDto(List<string> LotesCodigos, string DestinoCodigo);
    }
}
