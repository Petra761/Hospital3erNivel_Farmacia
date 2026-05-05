namespace DTOs
{
    public record RecetaReadDto(
        string Codigo,
        string PacienteCodigo,
        string PacienteNombre,
        string MedicoCodigo,
        string MedicoNombre,
        DateOnly FechaSolicitada,
        string Estado,
        List<DetalleRecetaReadDto> Detalles
    );

    public record RecetaPostDto(
        string PacienteCodigo,
        string MedicoCodigo,
        List<DetallesRecetaPostDto> Detalles
    );
}
