import { useState } from "react";
import { type ItemSeguimiento } from "../types/seguimiento";

export const SeguimientoItemRow = ({ item }: { item: ItemSeguimiento }) => {
  const [expanded, setExpanded] = useState(false);
  const progreso = (item.entregadoTotal / item.solicitado) * 100;

  return (
    <>
      <tr className="hover:bg-slate-50 transition-colors group">
        <td className="px-6 py-5">
          <div className="flex items-center gap-3">
            <div className="w-10 h-10 rounded-lg bg-secondary-container/30 flex items-center justify-center text-primary">
              <span className="material-symbols-outlined">pill</span>
            </div>
            <div className="font-bold text-on-surface">
              {item.medicamentoNombre}
            </div>
          </div>
        </td>
        <td className="px-6 py-5 font-medium">{item.solicitado} Unidades</td>
        <td className="px-6 py-5 min-w-[150px]">
          <div className="flex flex-col gap-1">
            <div className="flex justify-between text-[11px] font-bold">
              <span>
                {item.entregadoTotal} de {item.solicitado}
              </span>
              <span>{progreso.toFixed(0)}%</span>
            </div>
            <div className="w-full bg-slate-200 h-1.5 rounded-full">
              <div
                className={`h-full rounded-full transition-all duration-500 ${progreso === 100 ? "bg-primary" : "bg-orange-500"}`}
                style={{ width: `${progreso}%` }}
              ></div>
            </div>
          </div>
        </td>
        <td className="px-6 py-5 text-center">
          <span
            className={`px-2 py-1 rounded text-[10px] font-black uppercase ${
              item.estadoItem.includes("Total")
                ? "bg-primary/10 text-primary"
                : "bg-orange-100 text-orange-700"
            }`}
          >
            {item.estadoItem}
          </span>
        </td>
        <td className="px-6 py-5 text-right">
          <button
            onClick={() => setExpanded(!expanded)}
            className="p-2 rounded-lg hover:bg-surface-container-high text-primary transition-all"
          >
            <span
              className="material-symbols-outlined transition-transform"
              style={{
                transform: expanded ? "rotate(180deg)" : "rotate(0deg)",
              }}
            >
              expand_more
            </span>
          </button>
        </td>
      </tr>

      {expanded && (
        <tr className="bg-slate-50/50">
          <td colSpan={5} className="px-12 py-4">
            <div className="bg-white rounded-lg border border-outline-variant shadow-sm overflow-hidden">
              <div className="bg-slate-100 px-4 py-2 border-b border-outline-variant text-[10px] font-bold uppercase text-on-surface-variant">
                Historial de Entregas Realizadas
              </div>
              <table className="w-full text-xs text-left">
                <thead>
                  <tr className="border-b border-outline-variant text-outline">
                    <th className="px-4 py-2">Fecha</th>
                    <th className="px-4 py-2">Lote</th>
                    <th className="px-4 py-2 text-center">Cant.</th>
                    <th className="px-4 py-2">Responsable</th>
                  </tr>
                </thead>
                <tbody>
                  {item.historialEntrega.map((h, i) => (
                    <tr key={i} className="border-b border-outline-variant/30">
                      <td className="px-4 py-2">
                        {new Date(h.fechaEntregado).toLocaleString()}
                      </td>
                      <td className="px-4 py-2 font-mono">{h.loteCodigo}</td>
                      <td className="px-4 py-2 text-center font-bold text-primary">
                        {h.cantidad}
                      </td>
                      <td className="px-4 py-2">{h.farmaceutico}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            </div>
          </td>
        </tr>
      )}
    </>
  );
};
