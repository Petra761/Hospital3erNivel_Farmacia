namespace DTOs
{
    public record DetalleRecetaReadDto(
        string MedicamentoCodigo,
        string MedicamentoNombre,
        int CantidadSolicitada,
        string Estado,
        PosologiaReadDto Posologia
    );

    public record DetallesRecetaPostDto(
        string MedicamentoCodigo,
        int CantidadSolicitada,
        PosologiaPostDto Posologia
    );
}
