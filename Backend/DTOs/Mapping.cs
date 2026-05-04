using Models;
using Npgsql.Internal;

namespace DTOs
{
    public static class FormaFarmaceuticaMapping
    {
        public static FormaFarmaceuticaReadDto ToReadDto(this FormaFarmaceutica ff)
        {
            return new FormaFarmaceuticaReadDto(ff.Nombre);
        }
    }

    public static class TipoUnidadMedidaMapping
    {
        public static TipoUnidadMedidaReadDto ToReadDto(this TipoUnidadMedida tp)
        {
            return new TipoUnidadMedidaReadDto(tp.Nombre, tp.Abreviatura);
        }
    }

    public static class TipoMedicamentoMapping
    {
        public static TipoMedicamentoReadDto ToReadDto(this TipoMedicamento tm)
        {
            return new TipoMedicamentoReadDto(
                tm.Codigo,
                tm.NombreGenerico,
                tm.NombreComercial,
                tm.StockMinimoAlerta
            );
        }
    }

    public static class MedicamentoMapping
    {
        public static MedicamentoReadDto ToReadDto(this Medicamento m)
        {
            return new MedicamentoReadDto(
                m.Codigo,
                m.TipoMedicamento.NombreGenerico,
                m.TipoMedicamento.NombreComercial,
                m.TipoUnidadMedida.Nombre,
                m.FormaFarmaceutica.Nombre,
                m.ValorConcentracion.ToString("N2")
            );
        }
    }

    public static class UbicacionAlmacenMapping
    {
        public static UbicacionAlmacenReadDto ToReadDto(this UbicacionAlmacen ua)
        {
            return new UbicacionAlmacenReadDto(ua.Codigo, ua.Nombre);
        }
    }

    public static class TipoMovimientoMappinng
    {
        public static TipoMovimientoDto ToDto(this TipoMovimiento tm)
        {
            return new TipoMovimientoDto(tm.Descripcion);
        }
    }

    public static class RecepcionMapping
    {
        public static RecepcionReadDto ToReadDto(this Recepcion r)
        {
            return new RecepcionReadDto(r.Codigo, r.FechaRecepcion.ToString(), r.RecibidoPorCodigo);
        }
    }

    public static class DetalleRecepcionMapping
    {
        public static DetalleRecepcionReadDto ToReadDto(this DetalleRecepcion dr)
        {
            return new DetalleRecepcionReadDto(
                dr.Recepcion.Codigo,
                dr.Medicamento.TipoMedicamento.NombreComercial,
                dr.CantidadRecibida
            );
        }
    }

    public static class StockActualMapping
    {
        public static StockActualReadDto ToReadDto(this StockActual s)
        {
            var med = s.Lote.Medicamento;
            var tipo = med.TipoMedicamento;

            return new StockActualReadDto(
                s.Ubicacion.Codigo,
                s.Ubicacion.Nombre,
                s.Cantidad,
                med.Codigo,
                tipo.NombreComercial,
                $"{med.ValorConcentracion} {med.TipoUnidadMedida.Abreviatura}",
                med.FormaFarmaceutica.Nombre,
                s.Lote.Codigo,
                s.Lote.DetalleRecepcion.FechaVencimiento,
                tipo.EsControlado,
                tipo.RequiereRefrigeracion
            );
        }
    }

    public static class MovimientoMapping
    {
        public static MovimientoReadDto ToReadDto(this Movimiento m)
        {
            return new MovimientoReadDto(
                m.Codigo,
                m.StockActual.Lote.Codigo,
                m.TipoMovimiento.Descripcion,
                m.Cantidad,
                m.Fecha,
                m.Observaciones
            );
        }
    }
}
