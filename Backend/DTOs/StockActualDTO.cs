namespace DTOs
{
    public record StockActualReadDto(
        string UbicacionCodigo,
        string UbicacionNombre,
        int CantidadDisponible,
        string MedicamentoCodigo,
        string MedicamentoNombre,
        string Concentracion,
        string Forma,
        string LoteCodigo,
        DateOnly FechaVencimiento,
        bool EsControlado,
        bool RequiereRefrigeracion
    );
}
