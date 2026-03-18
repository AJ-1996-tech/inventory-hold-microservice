import InventoryDashboard from "./components/InventoryDashboard";
import CreateHold from "./components/CreateHold";
import HoldsList from "./components/HoldsList";

function App() {
  return (
    <div style={{ padding: 20 }}>
      <h1>Inventory Hold System</h1>

      <InventoryDashboard />
      <hr />
      <CreateHold />
      <hr />
      <HoldsList />
    </div>
  );
}

export default App;