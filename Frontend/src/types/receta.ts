export interface Posologia {
  dosis: string;
  viaAdministracion: string;
  frecuencia: string;
  duracion: string;
  indicacionesAdicionales: string;
}

export interface DetalleReceta {
  medicamentoCodigo: string;
  medicamentoNombre: string;
  cantidadSolicitada: number;
  estado: string;
  posologia: Posologia;
}

export interface Receta {
  codigo: string;
  pacienteCodigo: string;
  pacienteNombre: string;
  medicoCodigo: string;
  medicoNombre: string;
  fechaSolicitada: string;
  estado: string;
  detalles: DetalleReceta[];
}
