import { useState, useEffect } from "react";
import { type StockItem } from "../types/stock";
import { getStocks } from "../services/stockService";

export const useStock = () => {
  const [stocks, setStocks] = useState<StockItem[]>([]);
  const [selectedItem, setSelectedItem] = useState<StockItem | null>(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    getStocks()
      .then((data) => {
        setStocks(data);
        if (data.length > 0) setSelectedItem(data[0]);
      })
      .finally(() => setLoading(false));
  }, []);

  return { stocks, selectedItem, setSelectedItem, loading };
};
