import { useQuery } from "@tanstack/react-query";

export type Seance = {
  id_seance: number;
  date_seance: string;
  fk_film: number;
  fk_salle: number;
};

export const useFilmSeances = (filmId: number) => {
  return useQuery<Seance[]>({
    queryKey: ["seances", filmId],
    queryFn: async () => {
      const res = await fetch(
        `http://35.181.160.232:5000/api/Seance/${filmId}`
      );
      if (!res.ok) throw new Error("Erreur lors du chargement des séances");
      return res.json();
    },
    enabled: !!filmId,
  });
};
