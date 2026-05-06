import { useState } from "react";
import { type Receta } from "../types/receta";
import { RecetaDetail } from "./RecetaDetail";

interface Props {
  receta: Receta;
}

export const RecetaRow = ({ receta }: Props) => {
  const [isExpanded, setIsExpanded] = useState(false);

  return (
    <>
      <tr
        className={`hover:bg-surface-container-low/50 transition-all cursor-pointer group ${isExpanded ? "bg-primary/5 border-l-4 border-primary" : ""}`}
        onClick={() => setIsExpanded(!isExpanded)}
      >
        <td className="px-6 py-5 font-mono text-primary font-bold">
          {receta.codigo}
        </td>
        <td className="px-6 py-5 font-medium">{receta.pacienteNombre}</td>
        <td className="px-6 py-5 text-on-surface-variant">
          {receta.medicoNombre}
        </td>
        <td className="px-6 py-5 text-on-surface-variant">
          {new Date(receta.fechaSolicitada).toLocaleDateString("es-ES", {
            day: "2-digit",
            month: "short",
            year: "numeric",
          })}
        </td>
        <td className="px-6 py-5">
          <span
            className={`px-3 py-1 rounded-full text-[11px] font-bold uppercase tracking-widest ${
              receta.estado === "Pendiente"
                ? "bg-primary-container text-on-primary-container"
                : "bg-secondary-container text-on-secondary-container"
            }`}
          >
            {receta.estado}
          </span>
        </td>
        <td className="px-6 py-5 text-right">
          <button className="text-primary hover:bg-primary/10 p-1 rounded-full transition-all shadow-sm">
            <span
              className="material-symbols-outlined block transition-transform duration-300"
              style={{
                transform: isExpanded ? "rotate(180deg)" : "rotate(0deg)",
              }}
            >
              expand_more
            </span>
          </button>
        </td>
      </tr>

      {isExpanded && (
        <tr>
          <td colSpan={6} className="px-6 pb-8 pt-0 bg-primary/5">
            <RecetaDetail receta={receta} />
          </td>
        </tr>
      )}
    </>
  );
};
