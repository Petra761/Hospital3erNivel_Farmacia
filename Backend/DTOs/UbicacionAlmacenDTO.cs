namespace DTOs
{
    public record UbicacionAlmacenReadDto(string Codigo, string Nombre);

    public record UbicacionAlmacenPostDto(string Nombre);

    public record ReporteStockUbicacionDto(
        string UbicacionCodigo,
        string UbicacionNombre,
        List<ItemStockUbicacionDto> Items
    );

    public record ItemStockUbicacionDto(
        string MedicamentoNombre,
        string LoteCodigo,
        int Cantidad,
        DateOnly FechaVencimiento,
        string Presentacion,
        string EstadoLote
    );
}
