import { useQuery } from "@tanstack/react-query";

const API_URL = "http://35.181.160.232:5000/api";

export type Place = {
  id_place: number;
  numero_place: number;
  disponibilite: boolean;
  fk_salle: number;
  fk_reservation: number | null;
};

export type Salle = {
  id_salle: number;
  numero_salle: number;
  capacite: number;
  disponibilite_salle: boolean;
  type: string;
  presenteDans: Place[];
};

export const useSalleFromSeanceId = (seanceId: number) => {
  const fetchSalle = async (): Promise<Salle> => {
    const userData = localStorage.getItem("user");
    const fallbackToken = localStorage.getItem("token");
    let token = null;

    try {
      token = userData ? JSON.parse(userData)?.token : fallbackToken;
    } catch (e) {
      console.error("Erreur de parsing du token:", e);
      token = fallbackToken;
    }

    console.log("➡️ Token utilisé:", token);

    if (!token) {
      throw new Error("Token d'authentification manquant.");
    }

    // Étape 1 : récupérer la séance
    const seanceResponse = await fetch(`${API_URL}/Seance/${seanceId}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
      credentials: "include",
    });

    if (!seanceResponse.ok) {
      throw new Error("Erreur lors de la récupération de la séance.");
    }

    const seance = await seanceResponse.json();
    const salleId = seance.fk_salle;

    // Étape 2 : récupérer la salle associée
    const salleResponse = await fetch(`${API_URL}/Salle/${salleId}`, {
      method: "GET",
      headers: {
        Authorization: `Bearer ${token}`,
        "Content-Type": "application/json",
      },
      credentials: "include",
    });

    if (!salleResponse.ok) {
      throw new Error("Erreur lors de la récupération de la salle.");
    }

    return salleResponse.json();
  };

  return useQuery<Salle, Error>({
    queryKey: ["salle-from-seance", seanceId],
    queryFn: fetchSalle,
    enabled: !!seanceId,
  });
};
