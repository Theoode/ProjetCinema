import { useQuery } from "@tanstack/react-query";

export type Movie = {
  id_film: number;
  nom_film: string;
  auteur: string;
  description: string;
  duree: string;
  date_sortie: string;
  affiche: string;
};

const API_URL = "http://35.181.160.232:5000/api";

export const useNowPlayingMovies = () => {
  const fetchNowPlaying = async (): Promise<Movie[]> => {
    const res = await fetch(`${API_URL}/Film`);
    if (!res.ok) {
      throw new Error("Erreur lors du chargement des films à l'affiche");
    }
    return res.json();
  };

  return useQuery<Movie[], Error>({
    queryKey: ["nowPlayingMovies"],
    queryFn: fetchNowPlaying,
  });
};
