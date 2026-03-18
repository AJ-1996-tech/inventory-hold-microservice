import { create } from "zustand";
import { api } from "../api/api";
import { InventoryItem } from "../types";

interface State {
  inventory: InventoryItem[];
  loading: boolean;
  fetchInventory: () => Promise<void>;
}

export const useInventoryStore = create<State>((set) => ({
  inventory: [],
  loading: false,

  fetchInventory: async () => {
    set({ loading: true });

    try {
      const res = await api.get("/inventory");
      set({ inventory: res.data });
    } catch (err) {
      console.error(err);
    }

    set({ loading: false });
  },
}));