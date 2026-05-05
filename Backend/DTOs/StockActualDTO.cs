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

    public record BajoStockReadDto(
        string MedicamentoCodigo,
        string NombreMedicamento,
        int StockActualTotal,
        int StockMinimoAlerta,
        int CantidadFaltante,
        string NivelUrgencia
    );

    public record StockPorMedicamentoReadDto(
        string Codigo,
        string NombreCompleto,
        string Concentracion,
        string Forma,
        int CantidadDisponible,
        string Unidad,
        string Alerta
    );
}
