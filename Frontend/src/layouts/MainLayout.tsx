import { type ReactNode } from "react";
import { Navbar } from "../componets/Navbar";

interface Props {
  children: ReactNode;
}

export const MainLayout = ({ children }: Props) => {
  return (
    <div className="min-h-screen bg-background flex flex-col">
      <Navbar />
      <div className="flex-grow">{children}</div>
    </div>
  );
};
