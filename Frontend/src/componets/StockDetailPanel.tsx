import { type StockItem } from "../types/stock";

interface Props {
  item: StockItem | null;
}

export const StockDetailPanel = ({ item }: Props) => {
  if (!item)
    return (
      <div className="p-8 text-center text-outline">
        Seleccione un medicamento
      </div>
    );

  return (
    <aside className="space-y-6 animate-in fade-in slide-in-from-right-4 duration-300">
      <div className="bg-white rounded-xl border border-outline-variant p-6 shadow-sm">
        <div className="flex justify-between items-start mb-6">
          <div>
            <h2 className="text-2xl font-bold text-primary leading-tight">
              {item.medicamentoNombre}
            </h2>
            <span className="text-[10px] font-bold text-on-surface-variant uppercase tracking-widest">
              CÓDIGO: {item.medicamentoCodigo}
            </span>
          </div>
          <span className="material-symbols-outlined text-primary-container bg-primary-container/10 p-2 rounded-full">
            medical_services
          </span>
        </div>

        <div className="space-y-4">
          <div className="flex items-center gap-3 p-3 bg-surface-container-low rounded-lg">
            <span className="material-symbols-outlined text-secondary">
              kitchen
            </span>
            <div>
              <div className="text-[10px] font-bold text-on-surface-variant uppercase tracking-wide">
                Ubicación Actual
              </div>
              <div className="text-sm font-bold">{item.ubicacionNombre}</div>
            </div>
          </div>

          <div className="flex items-center gap-3 p-3 bg-surface-container-low rounded-lg">
            <span className="material-symbols-outlined text-primary">
              inventory_2
            </span>
            <div>
              <div className="text-[10px] font-bold text-on-surface-variant uppercase tracking-wide">
                Total Disponible
              </div>
              <div className="text-sm font-bold">
                {item.cantidadDisponible} unidades
              </div>
            </div>
          </div>

          <div className="flex flex-wrap gap-2 pt-2">
            {item.esControlado && (
              <div className="flex items-center gap-1 px-3 py-1 bg-error-container/20 text-error rounded-lg text-[11px] font-bold">
                <span className="material-symbols-outlined text-xs">lock</span>{" "}
                CONTROLADO
              </div>
            )}
            {item.requiereRefrigeracion && (
              <div className="flex items-center gap-1 px-3 py-1 bg-secondary-container/30 text-secondary rounded-lg text-[11px] font-bold">
                <span className="material-symbols-outlined text-xs">
                  ac_unit
                </span>{" "}
                REFRIGERACIÓN
              </div>
            )}
          </div>

          <div className="mt-6">
            <h3 className="text-[10px] font-bold text-on-surface-variant uppercase tracking-widest mb-3 italic">
              Información de Lote
            </h3>
            <div className="border border-outline-variant rounded-lg overflow-hidden">
              <table className="w-full text-left text-xs">
                <thead className="bg-surface-container-low">
                  <tr>
                    <th className="px-3 py-2 font-bold">Lote</th>
                    <th className="px-3 py-2 text-right">Cant.</th>
                    <th className="px-3 py-2">Vencimiento</th>
                  </tr>
                </thead>
                <tbody>
                  <tr>
                    <td className="px-3 py-3 font-mono">{item.loteCodigo}</td>
                    <td className="px-3 py-3 text-right font-bold">
                      {item.cantidadDisponible}
                    </td>
                    <td className="px-3 py-3 font-bold text-error">
                      {new Date(item.fechaVencimiento).toLocaleDateString()}
                    </td>
                  </tr>
                </tbody>
              </table>
            </div>
          </div>

          <div className="flex gap-2 pt-4">
            <button className="flex-1 py-3 bg-primary text-white rounded-xl font-bold text-sm hover:opacity-90 transition-all">
              Reponer Stock
            </button>
            <button className="px-3 py-3 border border-primary text-primary rounded-xl hover:bg-primary/5 transition-all">
              <span className="material-symbols-outlined text-sm">edit</span>
            </button>
          </div>
        </div>
      </div>

      <div className="bg-primary-container p-6 rounded-xl text-white shadow-lg relative overflow-hidden">
        <div className="relative z-10">
          <div className="flex items-center gap-2 mb-1 opacity-80 text-[10px] font-bold">
            <span className="material-symbols-outlined text-xs">
              trending_up
            </span>{" "}
            TENDENCIA DE CONSUMO
          </div>
          <div className="text-3xl font-bold">+12%</div>
          <p className="text-xs opacity-70 mt-1">Demanda aumentada este mes.</p>
        </div>
        <span className="material-symbols-outlined absolute -right-2 -bottom-2 text-white/10 text-7xl rotate-12">
          analytics
        </span>
      </div>
    </aside>
  );
};
