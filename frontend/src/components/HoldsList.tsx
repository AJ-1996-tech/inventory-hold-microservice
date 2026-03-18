import { useState } from "react";
import { useHoldStore } from "../store/holdStore";
import HoldItemCard from "./HoldItemCard";

export default function HoldsList() {
  const [holdId, setHoldId] = useState("");
  const { holds, fetchHold, releaseHold } = useHoldStore();

  return (
    <div>
      <h3>Find Hold</h3>

      <input
        placeholder="Enter Hold ID"
        onChange={(e) => setHoldId(e.target.value)}
      />

      <button onClick={() => fetchHold(holdId)}>Fetch</button>

      {holds.map((h) => (
        <HoldItemCard key={h.id} hold={h} onRelease={releaseHold} />
      ))}
    </div>
  );
}