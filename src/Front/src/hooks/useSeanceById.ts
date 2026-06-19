import { useQuery } from "@tanstack/react-query";

export type Seance = {
  id_seance: number;
  date_seance: string;
  fk_film: number;
  fk_salle: number;
};

const fetchSeanceById = async (seanceId: number): Promise<Seance> => {
  const res = await fetch("http://35.181.160.232:5000/api/Seance");
  if (!res.ok) throw new Error("Erreur de récupération des séances");

  const seances: Seance[] = await res.json();
  const seance = seances.find((s) => s.id_seance === seanceId);

  if (!seance) throw new Error("Séance non trouvée");
  return seance;
};

export const useSeanceById = (seanceId: number) => {
  return useQuery({
    queryKey: ["seance", seanceId],
    queryFn: () => fetchSeanceById(seanceId),
    enabled: !!seanceId,
  });
};
