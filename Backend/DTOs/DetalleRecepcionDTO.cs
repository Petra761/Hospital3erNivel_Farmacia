namespace DTOs
{
    public record DetalleRecepcionReadDto(
        string RecepcionCodigo,
        string MedicamentoNombre,
        int CantidadRecibida
    );

    public record DatalleRecepcionPostDto(
        string MedicamentoCodigo,
        int CantidadRecibida,
        string Estado,
        DateOnly FechaVencimiento
    );
}
