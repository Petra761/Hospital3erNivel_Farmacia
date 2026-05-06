import { type StockItem } from "../types/stock";

const API_URL =
  "https://hospital3ernivel-farmacia.onrender.com/apiStocksActuales";

export const getStocks = async (): Promise<StockItem[]> => {
  const response = await fetch(API_URL);
  if (!response.ok) throw new Error("Error al obtener el stock");
  return await response.json();
};
