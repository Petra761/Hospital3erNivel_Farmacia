export interface StockItem {
  ubicacionCodigo: string;
  ubicacionNombre: string;
  cantidadDisponible: number;
  medicamentoCodigo: string;
  medicamentoNombre: string;
  concentracion: string;
  forma: string;
  loteCodigo: string;
  fechaVencimiento: string;
  esControlado: boolean;
  requiereRefrigeracion: boolean;
}
