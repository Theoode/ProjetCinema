import { useEffect, useState } from "react";

export const useUserReservations = (userId: string | undefined) => {
  const [data, setData] = useState<any[]>([]);
  const [isLoading, setIsLoading] = useState(true);

  useEffect(() => {
    const stored = JSON.parse(localStorage.getItem("reservations") ?? "[]");

    const filtered = stored;

    setTimeout(() => {
      setData(filtered);
      setIsLoading(false);
    }, 300);
  }, [userId]);

  return { data, isLoading };
};

export const useReservationDetails = (reservationId: string | undefined) => {
  const [reservation, setReservation] = useState<any | null>(null);

  useEffect(() => {
    if (!reservationId) return;
    const stored = JSON.parse(localStorage.getItem("reservations") ?? "[]");
    const found = stored.find((r: any) => r.id === reservationId);
    setReservation(found ?? null);
  }, [reservationId]);

  return reservation;
};
