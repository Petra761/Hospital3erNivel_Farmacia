import { type SeguimientoResponse } from "../types/seguimiento";

const API_BASE_URL =
  "https://hospital3ernivel-farmacia.onrender.com/api/Recetas/seguimiento";

export const getSeguimientoByCodigo = async (
  codigo: string,
): Promise<SeguimientoResponse> => {
  const response = await fetch(`${API_BASE_URL}/${codigo}`);
  if (!response.ok)
    throw new Error("No se encontró la receta o el código es inválido");
  return await response.json();
};
