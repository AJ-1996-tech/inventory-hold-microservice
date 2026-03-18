import { useEffect } from "react";
import { useInventoryStore } from "../store/inventoryStore";

export default function InventoryDashboard() {
  const { inventory, fetchInventory, loading } = useInventoryStore();

  useEffect(() => {
    fetchInventory();
  }, []);

  return (
    <div>
      <h2>Inventory</h2>

      {loading && <p>Loading...</p>}

      {inventory.map((item) => (
        <div key={item.id}>
          <b>{item.productName}</b> - Qty: {item.quantity}
        </div>
      ))}
    </div>
  );
}