import { useState } from "react";
import { api } from "../api/api";
import { useInventoryStore } from "../store/inventoryStore";

export default function CreateHold() {
  const [productId, setProductId] = useState("");
  const [quantity, setQuantity] = useState(1);

  const { fetchInventory } = useInventoryStore();

  const createHold = async () => {
    try {
      const res = await api.post("/holds", {
        items: [{ productId, quantity }],
      });

      alert(`Hold created: ${res.data.holdId}`);

      fetchInventory();
    } catch (err: any) {
      alert(err.response?.data?.message || "Error creating hold");
    }
  };

  return (
    <div>
      <h3>Create Hold</h3>

      <input
        placeholder="Product ID"
        onChange={(e) => setProductId(e.target.value)}
      />

      <input
        type="number"
        value={quantity}
        onChange={(e) => setQuantity(Number(e.target.value))}
      />

      <button onClick={createHold}>Create Hold</button>
    </div>
  );
}