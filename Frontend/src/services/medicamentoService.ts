import { type Medicamento } from "../types/medicamento";

const API_URL = "https://hospital3ernivel-farmacia.onrender.com/api/Medicamentos/catalogo";

export const getMedicamentos = async (): Promise<Medicamento[]> => {
  const response = await fetch(API_URL);
  if (!response.ok) {
    throw new Error("Error al obtener los medicamentos");
  }
  return await response.json();
};