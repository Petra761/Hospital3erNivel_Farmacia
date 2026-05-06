import { Routes, Route } from "react-router-dom";
import { MainLayout } from "./layouts/MainLayout";
import CatalogoPage from "./pages/CatalogoPage";
import RecetasPage from "./pages/RecetasPage";
import StockPage from "./pages/StockPage";
import SeguimientoPage from "./pages/SeguimientoPage";

function App() {
  return (
    <MainLayout>
      <Routes>
        <Route path="/" element={<CatalogoPage />} />
        <Route path="/recetas" element={<RecetasPage />} />
        <Route path="/stock" element={<StockPage />} />
        <Route path="/seguimiento" element={<SeguimientoPage />} />
      </Routes>
    </MainLayout>
  );
}

export default App;
