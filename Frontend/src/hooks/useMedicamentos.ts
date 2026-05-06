import { useState, useEffect } from "react";
import { type Medicamento } from "../types/medicamento";
import { getMedicamentos } from "../services/medicamentoService";

export const useMedicamentos = () => {
  const [medicamentos, setMedicamentos] = useState<Medicamento[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    getMedicamentos()
      .then((data) => {
        setMedicamentos(data);
        setLoading(false);
      })
      .catch((err) => {
        setError(err.message);
        setLoading(false);
      });
  }, []);

  return { medicamentos, loading, error };
};
