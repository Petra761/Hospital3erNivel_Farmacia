namespace DTOs
{
    public record MovimientoReadDto(
        string Codigo,
        string CodigoLote,
        string TipoMovimiento,
        int Cantidad,
        DateOnly Fecha,
        string Observacion
    );
}
