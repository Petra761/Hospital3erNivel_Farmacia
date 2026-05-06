import { useState } from "react";
import { useSeguimiento } from "../hooks/useSeguimiento";
import { SeguimientoItemRow } from "../componets/SeguimientoItemRow";

export default function SeguimientoPage() {
  const [codigo, setCodigo] = useState("");
  const { data, loading, error, buscarReceta } = useSeguimiento();

  return (
    <main className="max-w-[1440px] mx-auto w-full px-container-padding py-stack-lg flex flex-col gap-8">
      {/* Buscador */}
      <section className="bg-surface-container-lowest border border-outline-variant rounded-xl p-6 shadow-sm flex flex-col md:flex-row items-end gap-4">
        <div className="flex-1 w-full">
          <label className="text-[11px] font-bold uppercase text-on-surface-variant block mb-2 tracking-widest">
            Código de Receta
          </label>
          <div className="relative">
            <span className="material-symbols-outlined absolute left-4 top-1/2 -translate-y-1/2 text-outline">
              search
            </span>
            <input
              className="w-full pl-12 pr-4 py-3 bg-slate-100 border-none rounded-lg focus:ring-2 focus:ring-primary outline-none"
              placeholder="Ej: RCT-XXXX-XXXX"
              type="text"
              value={codigo}
              onChange={(e) => setCodigo(e.target.value)}
            />
          </div>
        </div>
        <button
          onClick={() => buscarReceta(codigo)}
          disabled={loading}
          className="bg-primary text-on-primary px-8 py-3 rounded-lg font-bold hover:opacity-90 active:scale-95 transition-all flex items-center gap-2 h-[52px]"
        >
          {loading ? (
            "Buscando..."
          ) : (
            <>
              <span className="material-symbols-outlined">analytics</span>{" "}
              Buscar
            </>
          )}
        </button>
      </section>

      {error && (
        <div className="p-4 bg-error-container text-error rounded-xl font-bold">
          {error}
        </div>
      )}

      {data && (
        <div className="grid grid-cols-1 lg:grid-cols-12 gap-8 animate-in fade-in duration-500">
          {/* Resumen Izquierda */}
          <div className="lg:col-span-4 flex flex-col gap-6">
            <article className="bg-surface-container-lowest border border-outline-variant rounded-xl p-6 shadow-sm">
              <div className="flex justify-between items-start mb-6">
                <h2 className="text-xl font-bold">Detalle de Receta</h2>
                <span className="bg-orange-100 text-orange-800 px-3 py-1 rounded-full text-[10px] font-black uppercase tracking-widest">
                  {data.estadoGeneral}
                </span>
              </div>
              <div className="space-y-6">
                {[
                  {
                    icon: "receipt_long",
                    label: "Código",
                    val: data.codigoReceta,
                  },
                  {
                    icon: "person",
                    label: "Paciente",
                    val: data.pacienteNombre,
                  },
                  {
                    icon: "medical_information",
                    label: "Médico",
                    val: data.medicoNombre,
                  },
                ].map((item, i) => (
                  <div key={i} className="flex items-center gap-4">
                    <div className="w-10 h-10 rounded-lg bg-slate-100 flex items-center justify-center text-primary">
                      <span className="material-symbols-outlined">
                        {item.icon}
                      </span>
                    </div>
                    <div>
                      <p className="text-[10px] font-bold text-on-surface-variant uppercase">
                        {item.label}
                      </p>
                      <p className="font-bold text-sm">{item.val}</p>
                    </div>
                  </div>
                ))}
              </div>
            </article>

            <div className="bg-primary text-on-primary rounded-xl p-6 relative overflow-hidden">
              <span className="material-symbols-outlined text-4xl mb-4 relative z-10">
                info
              </span>
              <p className="font-bold mb-1 relative z-10">Estado del Proceso</p>
              <p className="text-xs opacity-80 relative z-10">
                Esta receta se encuentra en fase de {data.estadoGeneral}.
              </p>
              <span className="material-symbols-outlined absolute -right-4 -bottom-4 text-9xl opacity-10">
                medication
              </span>
            </div>
          </div>

          {/* Tabla Derecha */}
          <div className="lg:col-span-8">
            <section className="bg-surface-container-lowest border border-outline-variant rounded-xl overflow-hidden shadow-sm">
              <table className="w-full text-left">
                <thead className="bg-slate-50 border-b border-outline-variant">
                  <tr>
                    <th className="px-6 py-4 text-[10px] font-bold uppercase text-on-surface-variant">
                      Medicamento
                    </th>
                    <th className="px-6 py-4 text-[10px] font-bold uppercase text-on-surface-variant">
                      Solicitado
                    </th>
                    <th className="px-6 py-4 text-[10px] font-bold uppercase text-on-surface-variant">
                      Progreso
                    </th>
                    <th className="px-6 py-4 text-[10px] font-bold uppercase text-on-surface-variant text-center">
                      Estado
                    </th>
                    <th className="px-6 py-4 text-right"></th>
                  </tr>
                </thead>
                <tbody className="divide-y divide-outline-variant/30">
                  {data.items.map((item, i) => (
                    <SeguimientoItemRow key={i} item={item} />
                  ))}
                </tbody>
              </table>
            </section>
          </div>
        </div>
      )}
    </main>
  );
}
