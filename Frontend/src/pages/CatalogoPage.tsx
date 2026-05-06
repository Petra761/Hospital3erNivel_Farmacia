import { useMedicamentos } from "../hooks/useMedicamentos";
import { MedicamentoTable } from "../componets/MedicamentoTable";

export default function CatalogoPage() {
  const { medicamentos, loading, error } = useMedicamentos();

  if (error) return <div className="p-10 text-error">Error: {error}</div>;

  return (
    <main className="max-w-[1440px] mx-auto px-container-padding py-stack-lg flex flex-col gap-6">
      <div className="flex flex-col md:flex-row md:items-end justify-between gap-4">
        <div>
          <h1 className="text-4xl font-bold text-on-surface mb-2 tracking-tight">
            Catálogo de Medicamentos
          </h1>
          <p className="text-on-surface-variant">
            Gestione el inventario maestro y especificaciones clínicas.
          </p>
        </div>
        <button className="bg-primary text-on-primary px-6 py-2.5 rounded-lg flex items-center gap-2 font-medium hover:opacity-90 active:scale-95 transition-all shadow-sm">
          <span className="material-symbols-outlined text-[20px]">add</span>
          Nuevo Producto
        </button>
      </div>

      <div className="bg-surface-container-lowest border border-outline-variant rounded-xl p-4 shadow-sm">
        <div className="relative w-full max-w-2xl">
          <span className="material-symbols-outlined absolute left-4 top-1/2 -translate-y-1/2 text-outline">
            search
          </span>
          <input
            className="w-full pl-12 pr-4 py-3 bg-[#F1F5F9] border-none focus:ring-2 focus:ring-primary rounded-lg text-on-surface"
            placeholder="Buscar medicamento..."
            type="text"
          />
        </div>
      </div>

      {loading ? (
        <div className="flex justify-center py-20">
          <div className="animate-spin rounded-full h-10 w-10 border-b-2 border-primary"></div>
        </div>
      ) : (
        <MedicamentoTable data={medicamentos} />
      )}
    </main>
  );
}
