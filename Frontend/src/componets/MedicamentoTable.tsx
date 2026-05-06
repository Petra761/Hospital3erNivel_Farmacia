import { type Medicamento } from "../types/medicamento";

interface Props {
  data: Medicamento[];
}

export const MedicamentoTable = ({ data }: Props) => {
  const copiarAlPortapapeles = (texto: string) => {
    navigator.clipboard.writeText(texto);
    console.log("Copiado:", texto);
  };

  return (
    <div className="bg-surface-container-lowest border border-outline-variant rounded-xl shadow-sm overflow-hidden">
      <div className="overflow-x-auto">
        <table className="w-full text-left border-collapse">
          <thead>
            <tr className="bg-surface-container-low border-b border-outline-variant">
              <th className="px-6 py-4 text-xs font-bold uppercase tracking-wider text-on-surface-variant">
                Código
              </th>
              <th className="px-6 py-4 text-xs font-bold uppercase tracking-wider text-on-surface-variant">
                Nombre Genérico
              </th>
              <th className="px-6 py-4 text-xs font-bold uppercase tracking-wider text-on-surface-variant">
                Nombre Comercial
              </th>
              <th className="px-6 py-4 text-xs font-bold uppercase tracking-wider text-on-surface-variant">
                Forma
              </th>
              <th className="px-6 py-4 text-xs font-bold uppercase tracking-wider text-on-surface-variant">
                Concentración
              </th>
              <th className="px-6 py-4 text-xs font-bold uppercase tracking-wider text-on-surface-variant text-right">
                Acciones
              </th>
            </tr>
          </thead>
          <tbody className="divide-y divide-outline-variant/30">
            {data.map((med) => (
              <tr
                key={med.codigo}
                className="hover:bg-slate-50 transition-colors group"
              >
                <td className="px-6 py-4 font-mono text-sm text-on-surface">
                  <div className="flex items-center gap-2">
                    <span>{med.codigo}</span>
                    <button
                      onClick={() => copiarAlPortapapeles(med.codigo)}
                      className="material-symbols-outlined text-outline-variant hover:text-primary text-[18px] opacity-0 group-hover:opacity-100 transition-all cursor-pointer"
                      title="Copiar código"
                    >
                      content_copy
                    </button>
                  </div>
                </td>
                <td className="px-6 py-4 font-medium text-on-surface">
                  {med.nombreGenerico}
                </td>
                <td className="px-6 py-4 text-on-surface-variant">
                  {med.nombreComercial}
                </td>
                <td className="px-6 py-4">
                  <span className="bg-secondary-container text-on-secondary-container px-3 py-1 rounded-full text-[10px] font-bold uppercase tracking-wider">
                    {med.formaNombre}
                  </span>
                </td>
                <td className="px-6 py-4 text-on-surface-variant">
                  {med.valorConcentracion} {med.unidadMedida}
                </td>
                <td className="px-6 py-4 text-right text-on-surface-variant">
                  <button className="material-symbols-outlined hover:text-primary transition-colors cursor-pointer">
                    more_vert
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};
