import { useEffect, useState } from "react";

export default function HoldItemCard({ hold, onRelease }: any) {
  const [timeLeft, setTimeLeft] = useState("");

  useEffect(() => {
    const interval = setInterval(() => {
      const diff =
        new Date(hold.expiry).getTime() - new Date().getTime();

      if (diff <= 0) {
        setTimeLeft("Expired");
        return;
      }

      const mins = Math.floor(diff / 60000);
      const secs = Math.floor((diff % 60000) / 1000);

      setTimeLeft(`${mins}m ${secs}s`);
    }, 1000);

    return () => clearInterval(interval);
  }, [hold]);

  return (
    <div style={{ border: "1px solid gray", padding: 10, margin: 5 }}>
      <p><b>Hold ID:</b> {hold.id}</p>
      <p><b>Status:</b> {hold.isReleased ? "Released" : "Active"}</p>
      <p><b>Time Left:</b> {timeLeft}</p>

      {!hold.isReleased && (
        <button onClick={() => onRelease(hold.id)}>Release</button>
      )}
    </div>
  );
}