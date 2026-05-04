using Models;

namespace DTOs
{
    public record TipoMedicamentoReadDto(
        string Codigo,
        string NombreGenerico,
        string NombreComercial,
        int StockMinimoAlerta
    );

    public record TipoMedicamentoPostDto(
        string NombreGenerico,
        string NombreComercial,
        bool EsControlado,
        bool RequiereRefrigeracion,
        int StockMinimoAlerta
    );

    public record TipoMedicamentoPutDto(
        string NombreGenerico,
        string NombreComercial,
        bool EsControlado,
        bool RequiereRefrigeracion,
        int StockMinimoAlerta
    );

    public record TipoMedicamentoDetalleDto(
        string Codigo,
        string NombreGenerico,
        string NombreComercial,
        bool EsControlado,
        bool RequiereRefrigeracion,
        List<MedicamentoReadDto> Presentaciones
    );
}
