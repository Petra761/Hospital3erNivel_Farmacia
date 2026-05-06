export interface HistorialEntrega {
  fechaEntregado: string;
  loteCodigo: string;
  cantidad: number;
  farmaceutico: string;
}

export interface ItemSeguimiento {
  medicamentoNombre: string;
  solicitado: number;
  entregadoTotal: number;
  pendiente: number;
  estadoItem: string;
  historialEntrega: HistorialEntrega[];
}

export interface SeguimientoResponse {
  codigoReceta: string;
  pacienteNombre: string;
  medicoNombre: string;
  estadoGeneral: string;
  items: ItemSeguimiento[];
}
