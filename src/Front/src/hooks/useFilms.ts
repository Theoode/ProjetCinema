import { useQuery } from "@tanstack/react-query";

export type Film = {
  id_film: number;
  nom_film: string;
  auteur: string;
  description: string;
  duree: string;
  date_sortie: string;
  affiche: string;
};

export const useFilms = () => {
  return useQuery<Film[]>({
    queryKey: ["films"],
    queryFn: async () => {
      const res = await fetch("http://35.181.160.232:5000/api/Film");
      if (!res.ok) throw new Error("Erreur lors du chargement des films");
      return res.json();
    },
  });
};
