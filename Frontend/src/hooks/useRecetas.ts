import { useState, useEffect } from "react";
import { type Receta } from "../types/receta";
import { getRecetas } from "../services/recetaService";

export const useRecetas = () => {
  const [recetas, setRecetas] = useState<Receta[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    getRecetas()
      .then(setRecetas)
      .catch((err) => setError(err.message))
      .finally(() => setLoading(false));
  }, []);

  return { recetas, loading, error };
};
