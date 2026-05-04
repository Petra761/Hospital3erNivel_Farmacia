using Models;

namespace DTOs
{
    public record RecepcionReadDto(string Codigo, string FechaRecepcion, string RecibidoPor);

    public record RecepcionPostDto(
        string RecibidoPorCodigo,
        string Estado,
        List<DatalleRecepcionPostDto> Detalles
    );
}
