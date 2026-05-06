import { type Receta } from "../types/receta";

const API_URL = "https://hospital3ernivel-farmacia.onrender.com/api/Recetas";

export const getRecetas = async (): Promise<Receta[]> => {
  const response = await fetch(API_URL);
  if (!response.ok) throw new Error("Error al obtener recetas");
  return await response.json();
};
