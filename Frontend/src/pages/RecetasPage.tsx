import { useRecetas } from "../hooks/useRecetas";
import { RecetaRow } from "../componets/RecetaRow";

export default function RecetasPage() {
  const { recetas, loading } = useRecetas();

  return (
    <main className="max-w-[1440px] mx-auto px-container-padding py-stack-lg">
      <section className="flex flex-col md:flex-row md:items-end justify-between gap-6 mb-8">
        <div>
          <h1 className="text-4xl font-bold text-on-surface tracking-tight">
            Gestión de Recetas
          </h1>
          <p className="text-on-surface-variant mt-2">
            Administre y consulte las recetas médicas de manera eficiente.
          </p>
        </div>
        <button className="flex items-center gap-2 bg-primary text-on-primary px-6 py-3 rounded-xl font-bold hover:opacity-90 transition-all shadow-md">
          <span className="material-symbols-outlined">add_circle</span>
          Nueva Receta
        </button>
      </section>

      <section className="bg-surface-container-lowest p-4 rounded-xl border border-outline-variant mb-6 flex flex-wrap gap-4 items-center shadow-sm">
        <div className="flex-1 min-w-[300px] relative">
          <span className="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-outline">
            search
          </span>
          <input
            className="w-full pl-10 pr-4 py-3 bg-surface-container-low border-none rounded-lg focus:ring-2 focus:ring-primary"
            placeholder="Buscar por paciente o código..."
            type="text"
          />
        </div>
        <button className="p-3 text-primary border border-primary rounded-lg hover:bg-primary/5 transition-colors">
          <span className="material-symbols-outlined">filter_list</span>
        </button>
      </section>

      <div className="bg-surface-container-lowest rounded-xl border border-outline-variant overflow-hidden shadow-sm">
        <div className="overflow-x-auto">
          <table className="w-full border-collapse">
            <thead>
              <tr className="bg-surface-container-low border-b border-outline-variant">
                <th className="px-6 py-4 text-left text-xs font-bold uppercase tracking-widest text-on-surface-variant">
                  Código
                </th>
                <th className="px-6 py-4 text-left text-xs font-bold uppercase tracking-widest text-on-surface-variant">
                  Paciente
                </th>
                <th className="px-6 py-4 text-left text-xs font-bold uppercase tracking-widest text-on-surface-variant">
                  Médico
                </th>
                <th className="px-6 py-4 text-left text-xs font-bold uppercase tracking-widest text-on-surface-variant">
                  Fecha
                </th>
                <th className="px-6 py-4 text-left text-xs font-bold uppercase tracking-widest text-on-surface-variant">
                  Estado
                </th>
                <th className="px-6 py-4 text-right text-xs font-bold uppercase tracking-widest text-on-surface-variant">
                  Acciones
                </th>
              </tr>
            </thead>
            <tbody className="divide-y divide-outline-variant/30">
              {loading ? (
                <tr>
                  <td
                    colSpan={6}
                    className="text-center py-20 animate-pulse text-primary font-bold"
                  >
                    Cargando recetas médicas...
                  </td>
                </tr>
              ) : (
                recetas.map((receta) => (
                  <RecetaRow key={receta.codigo} receta={receta} />
                ))
              )}
            </tbody>
          </table>
        </div>
      </div>
    </main>
  );
}
