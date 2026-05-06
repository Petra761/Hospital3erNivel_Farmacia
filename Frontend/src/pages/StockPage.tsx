import { useStock } from "../hooks/useStock";
import { StockDetailPanel } from "../componets/StockDetailPanel";

export default function StockPage() {
  const { stocks, selectedItem, setSelectedItem, loading } = useStock();

  return (
    <main className="max-w-[1440px] mx-auto px-container-padding py-stack-lg">
      <header className="mb-8">
        <h1 className="text-4xl font-bold text-on-surface tracking-tight">
          Gestión de Stock
        </h1>
        <p className="text-on-surface-variant mt-1">
          Controle el inventario, lotes y vencimientos de medicamentos.
        </p>
      </header>

      <div className="flex flex-col md:flex-row justify-between items-center gap-4 mb-8 bg-surface-container-lowest p-4 rounded-xl border border-outline-variant shadow-sm">
        <div className="flex items-center gap-4 w-full md:w-auto">
          <div className="relative flex-grow md:w-80">
            <span className="material-symbols-outlined absolute left-3 top-1/2 -translate-y-1/2 text-outline">
              search
            </span>
            <input
              className="w-full pl-10 pr-4 py-2.5 bg-surface-container-low border-none rounded-lg focus:ring-2 focus:ring-primary"
              placeholder="Buscar medicamento..."
              type="text"
            />
          </div>
        </div>
        <button className="w-full md:w-auto px-6 py-2.5 bg-primary text-white rounded-lg flex items-center justify-center gap-2 font-bold hover:opacity-90 transition-all">
          <span className="material-symbols-outlined">download</span> Exportar
          Inventario
        </button>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-3 gap-8">
        <div className="lg:col-span-2 bg-white rounded-xl border border-outline-variant overflow-hidden shadow-sm">
          <div className="overflow-x-auto">
            <table className="w-full text-left border-collapse">
              <thead>
                <tr className="bg-surface-container-low border-b border-outline-variant">
                  <th className="px-6 py-4 text-[11px] font-bold uppercase tracking-widest text-on-surface-variant">
                    Medicamento
                  </th>
                  <th className="px-6 py-4 text-[11px] font-bold uppercase tracking-widest text-on-surface-variant">
                    Forma
                  </th>
                  <th className="px-6 py-4 text-[11px] font-bold uppercase tracking-widest text-on-surface-variant text-right">
                    Cantidad
                  </th>
                  <th className="px-6 py-4 text-[11px] font-bold uppercase tracking-widest text-on-surface-variant">
                    Estado
                  </th>
                  <th className="px-6 py-4 text-[11px] font-bold uppercase tracking-widest text-on-surface-variant text-right">
                    Acción
                  </th>
                </tr>
              </thead>
              <tbody className="divide-y divide-outline-variant/30">
                {loading ? (
                  <tr>
                    <td
                      colSpan={5}
                      className="text-center py-20 font-bold text-primary animate-pulse"
                    >
                      Cargando inventario...
                    </td>
                  </tr>
                ) : (
                  stocks.map((item) => (
                    <tr
                      key={item.loteCodigo}
                      onClick={() => setSelectedItem(item)}
                      className={`cursor-pointer transition-colors ${selectedItem?.loteCodigo === item.loteCodigo ? "bg-primary/5 border-l-4 border-primary" : "hover:bg-slate-50"}`}
                    >
                      <td className="px-6 py-4">
                        <div className="font-bold text-primary">
                          {item.medicamentoNombre}
                        </div>
                        <div className="text-[10px] text-on-surface-variant font-bold">
                          {item.medicamentoCodigo}
                        </div>
                      </td>
                      <td className="px-6 py-4 text-sm text-on-surface-variant">
                        {item.forma}
                      </td>
                      <td className="px-6 py-4 text-right font-mono font-bold text-on-surface">
                        {item.cantidadDisponible}
                      </td>
                      <td className="px-6 py-4">
                        <span
                          className={`px-3 py-1 rounded-full text-[10px] font-black uppercase tracking-tighter ${
                            item.cantidadDisponible < 100
                              ? "bg-error-container text-on-error-container"
                              : "bg-primary-container/10 text-primary"
                          }`}
                        >
                          {item.cantidadDisponible < 100
                            ? "BAJO STOCK"
                            : "DISPONIBLE"}
                        </span>
                      </td>
                      <td className="px-6 py-4 text-right">
                        <button className="text-primary text-xs font-bold hover:underline">
                          Ver Detalle
                        </button>
                      </td>
                    </tr>
                  ))
                )}
              </tbody>
            </table>
          </div>
        </div>

        <StockDetailPanel item={selectedItem} />
      </div>

      <section className="mt-12 grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
        {[
          {
            label: "Total Medicamentos",
            value: stocks.length,
            icon: "inventory",
            color: "text-primary",
            bg: "bg-primary/10",
          },
          {
            label: "Stock Crítico",
            value: stocks.filter((s) => s.cantidadDisponible < 100).length,
            icon: "priority_high",
            color: "text-error",
            bg: "bg-error/10",
          },
          {
            label: "Próximos Vencimientos",
            value: 45,
            icon: "event_busy",
            color: "text-orange-600",
            bg: "bg-orange-50",
          },
          {
            label: "Rotación Mensual",
            value: "85%",
            icon: "refresh",
            color: "text-secondary",
            bg: "bg-secondary/10",
          },
        ].map((stat, i) => (
          <div
            key={i}
            className="bg-white p-6 rounded-xl border border-outline-variant flex items-center gap-4 shadow-sm"
          >
            <div
              className={`h-12 w-12 rounded-full ${stat.bg} flex items-center justify-center ${stat.color}`}
            >
              <span className="material-symbols-outlined">{stat.icon}</span>
            </div>
            <div>
              <div className="text-[10px] font-bold text-on-surface-variant uppercase tracking-widest">
                {stat.label}
              </div>
              <div className="text-2xl font-bold">{stat.value}</div>
            </div>
          </div>
        ))}
      </section>
    </main>
  );
}
