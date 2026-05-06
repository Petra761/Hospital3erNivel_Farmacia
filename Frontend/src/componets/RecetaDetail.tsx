import { type Receta } from "../types/receta";

interface Props {
  receta: Receta;
}

export const RecetaDetail = ({ receta }: Props) => {
  return (
    <div className="bg-surface-container-lowest rounded-xl border border-primary/20 shadow-lg p-6 animate-in fade-in slide-in-from-top-2 duration-300">
      <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
        <div className="lg:border-r border-outline-variant pr-6">
          <h4 className="text-[12px] font-bold text-primary uppercase tracking-widest mb-6 flex items-center gap-2">
            <span className="material-symbols-outlined text-sm">info</span>
            Información General
          </h4>

          <div className="space-y-6">
            <div>
              <p className="text-[10px] uppercase font-bold text-outline mb-1">
                Paciente
              </p>
              <p className="font-bold text-on-surface text-lg leading-tight">
                {receta.pacienteNombre}
              </p>
              <p className="text-sm text-on-surface-variant">
                ID: {receta.pacienteCodigo}
              </p>
            </div>

            <div>
              <p className="text-[10px] uppercase font-bold text-outline mb-1">
                Médico Emisor
              </p>
              <p className="font-semibold text-on-surface">
                {receta.medicoNombre}
              </p>
              <p className="text-sm text-on-surface-variant">
                Registro: {receta.medicoCodigo}
              </p>
            </div>

            <div className="grid grid-cols-2 gap-4">
              <div>
                <p className="text-[10px] uppercase font-bold text-outline mb-1">
                  Fecha Emisión
                </p>
                <p className="text-sm font-medium">
                  {new Date(receta.fechaSolicitada).toLocaleDateString()}
                </p>
              </div>
              <div>
                <p className="text-[10px] uppercase font-bold text-outline mb-1">
                  Estado Receta
                </p>
                <p className="text-sm font-bold text-primary">
                  {receta.estado}
                </p>
              </div>
            </div>
          </div>
        </div>

        <div className="lg:col-span-2">
          <h4 className="text-[12px] font-bold text-primary uppercase tracking-widest mb-6 flex items-center gap-2">
            <span className="material-symbols-outlined text-sm">
              medication
            </span>
            Medicamentos y Posología
          </h4>

          <div className="bg-surface-container-low rounded-lg overflow-hidden mb-6 border border-outline-variant/20">
            <table className="w-full text-sm">
              <thead className="bg-surface-container text-on-surface-variant text-[11px] font-bold uppercase">
                <tr>
                  <th className="px-4 py-2 text-left">Cód.</th>
                  <th className="px-4 py-2 text-left">Medicamento</th>
                  <th className="px-4 py-2 text-center">Cant.</th>
                  <th className="px-4 py-2 text-right">Estado</th>
                </tr>
              </thead>
              <tbody className="divide-y divide-outline-variant/10">
                {receta.detalles.map((detalle, idx) => (
                  <tr key={idx} className="bg-white/40">
                    <td className="px-4 py-3 font-mono text-xs">
                      {detalle.medicamentoCodigo}
                    </td>
                    <td className="px-4 py-3 font-semibold text-on-surface">
                      {detalle.medicamentoNombre}
                    </td>
                    <td className="px-4 py-3 text-center font-bold text-primary">
                      {detalle.cantidadSolicitada}
                    </td>
                    <td className="px-4 py-3 text-right">
                      <span className="text-[10px] font-bold text-primary bg-primary/10 px-2 py-0.5 rounded">
                        DISPONIBLE
                      </span>
                    </td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
            {receta.detalles.map((detalle, idx) => (
              <div
                key={idx}
                className="bg-primary/5 p-4 rounded-xl border border-primary/10 hover:border-primary/30 transition-colors"
              >
                <p className="text-primary font-black text-[10px] mb-3 uppercase tracking-tighter border-b border-primary/10 pb-1">
                  POSOLOGÍA: {detalle.medicamentoNombre}
                </p>
                <div className="grid grid-cols-2 gap-y-3 gap-x-2 text-xs">
                  <div>
                    <span className="text-on-surface-variant block text-[10px] uppercase font-medium">
                      Dosis
                    </span>{" "}
                    {detalle.posologia.dosis}
                  </div>
                  <div>
                    <span className="text-on-surface-variant block text-[10px] uppercase font-medium">
                      Vía
                    </span>{" "}
                    {detalle.posologia.viaAdministracion}
                  </div>
                  <div>
                    <span className="text-on-surface-variant block text-[10px] uppercase font-medium">
                      Frecuencia
                    </span>{" "}
                    {detalle.posologia.frecuencia}
                  </div>
                  <div>
                    <span className="text-on-surface-variant block text-[10px] uppercase font-medium">
                      Duración
                    </span>{" "}
                    {detalle.posologia.duracion}
                  </div>
                </div>
                {detalle.posologia.indicacionesAdicionales && (
                  <div className="mt-3 pt-3 border-t border-primary/10 italic text-on-surface-variant text-[13px] leading-snug">
                    "{detalle.posologia.indicacionesAdicionales}"
                  </div>
                )}
              </div>
            ))}
          </div>
        </div>
      </div>

      <div className="mt-8 pt-6 border-t border-outline-variant flex justify-end gap-4">
        <button className="px-6 py-2.5 border border-outline-variant rounded-xl font-bold text-sm hover:bg-surface-container-low transition-colors">
          Imprimir Comprobante
        </button>
        <button className="px-6 py-2.5 bg-primary text-on-primary rounded-xl font-bold text-sm shadow-md hover:opacity-90 transition-opacity">
          Proceder al Despacho
        </button>
      </div>
    </div>
  );
};
