namespace DTOs
{
    public record SeguimientoRecetaReadDto(
        string CodigoReceta,
        string PacienteNombre,
        string MedicoNombre,
        string EstadoGeneral,
        List<SeguimientoItemReadDto> Items
    );

    public record SeguimientoItemReadDto(
        string MedicamentoNombre,
        int Solicitado,
        int EntregadoTotal,
        int Pendiente,
        string EstadoItem,
        List<EntregaFisicaDto> HistorialEntrega
    );

    public record EntregaFisicaDto(
        DateOnly FechaEntregado,
        string LoteCodigo,
        int Cantidad,
        string Farmaceutico
    );

    public record DispensacionPostDto(string RecetaCodigo, string FarmaceuticoIdentificador);

    namespace DTOs
    {
        public record DispensacionEnfermeriaDto(string RecetaCodigo, string EnfermeraCodigo);
    }
}
