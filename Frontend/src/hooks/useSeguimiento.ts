import { useState } from "react";
import { type SeguimientoResponse } from "../types/seguimiento";
import { getSeguimientoByCodigo } from "../services/seguimientoService";

export const useSeguimiento = () => {
  const [data, setData] = useState<SeguimientoResponse | null>(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);

  const buscarReceta = async (codigo: string) => {
    if (!codigo) return;
    setLoading(true);
    setError(null);
    try {
      const res = await getSeguimientoByCodigo(codigo);
      setData(res);
    } catch (err: any) {
      setError(err.message);
      setData(null);
    } finally {
      setLoading(false);
    }
  };

  return { data, loading, error, buscarReceta };
};
