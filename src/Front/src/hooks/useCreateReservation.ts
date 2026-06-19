import { useMutation } from "@tanstack/react-query";

const API_URL = "http://35.181.160.232:5000/api/Reservation";

type PlaceDto = {
  id_place: number;
  numero_place: number;
  disponibilite: boolean;
  fk_salle: number;
  fk_reservation?: number | null;
};

export type ReservationPayload = {
  date_reservation: string;
  contientDans: PlaceDto[];
  fk_seance: number;
  fk_paiement: null;
};

const createReservation = async (reservation: ReservationPayload) => {
  const token = localStorage.getItem("token");

  const response = await fetch(API_URL, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify({
      ...reservation,
      fk_paiement: null,
    }),
  });

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(
      `Erreur lors de la réservation : ${response.status} – ${errorText}`
    );
  }

  return response.json();
};

export const useCreateReservation = () => {
  return useMutation({
    mutationFn: createReservation,
  });
};
