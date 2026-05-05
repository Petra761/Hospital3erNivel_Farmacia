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

    public record DispensacionEnfermeriaDto(string RecetaCodigo, string EnfermeraCodigo);

    public record DispensacionPorFarmaceuticoReadDto(
        string DispensacionCodigo,
        string RecetaCodigo,
        string PacienteNombre,
        DateOnly Fecha,
        string Estado,
        List<ItemEntregadoReadDto> ItemsEntregados
    );

    public record ItemEntregadoReadDto(
        string MedicamentoNombre,
        string LoteCodigo,
        int CantidadEntregada
    );
}
