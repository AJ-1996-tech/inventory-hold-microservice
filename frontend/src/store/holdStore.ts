import { create } from "zustand";
import { api } from "../api/api";
import { Hold } from "../types";

interface State {
  holds: Hold[];
  fetchHold: (id: string) => Promise<void>;
  releaseHold: (id: string) => Promise<void>;
}

export const useHoldStore = create<State>((set) => ({
  holds: [],

  fetchHold: async (id: string) => {
    try {
      const res = await api.get(`/holds/${id}`);
      set({ holds: [res.data] });
    } catch {
      console.error("Hold not found");
    }
  },

  releaseHold: async (id: string) => {
    await api.delete(`/holds/${id}`);
    set((state) => ({
      holds: state.holds.filter((h) => h.id !== id),
    }));
  },
}));