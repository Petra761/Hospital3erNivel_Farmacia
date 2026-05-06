import { NavLink } from "react-router-dom";

export const Navbar = () => {
  const linkStyles = ({ isActive }: { isActive: boolean }) =>
    isActive
      ? "text-primary font-bold border-b-2 border-primary pb-1 transition-all"
      : "text-on-surface-variant font-medium hover:text-primary transition-colors";

  return (
    <header className="bg-surface shadow-sm sticky top-0 z-50 border-b border-outline-variant/30">
      <div className="flex justify-between items-center w-full px-container-padding h-16 max-w-[1440px] mx-auto">
        <div className="flex items-center gap-8">
          <span className="text-2xl text-primary font-bold tracking-tight">
            Farmacia De La Esquina
          </span>

          <nav className="hidden md:flex items-center gap-6">
            <NavLink to="/" className={linkStyles}>
              Catálogo
            </NavLink>

            <NavLink to="/stock" className={linkStyles}>
              Stock
            </NavLink>

            <NavLink to="/recetas" className={linkStyles}>
              Recetas
            </NavLink>

            <NavLink to="/seguimiento" className={linkStyles}>
              Seguimiento
            </NavLink>
          </nav>
        </div>

        <div className="flex items-center gap-4">
          <button className="material-symbols-outlined text-on-surface-variant hover:bg-surface-container-low p-2 rounded-full transition-colors cursor-pointer">
            notifications
          </button>
          <div className="h-8 w-8 rounded-full bg-secondary-container overflow-hidden border border-outline-variant">
            <img
              src="https://i.pinimg.com/1200x/c8/7e/65/c87e6591818c49a40ab70b96bd034392.jpg"
              alt="Avatar"
              className="w-full h-full object-cover"
            />
          </div>
        </div>
      </div>
    </header>
  );
};
