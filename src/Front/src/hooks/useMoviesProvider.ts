import { useQuery } from "@tanstack/react-query";

const API_URL = "http://35.181.160.232:5000/api";

export type Seance = {
  id_seance: number;
  date_seance: string;
  fk_film: number;
  fk_salle: number;
  film: {
    id_film: number;
    nom_film: string;
  };
  salle: {
    id_salle: number;
    numero_salle: number;
  };
};

export type Movie = {
  id_film: number;
  nom_film: string;
  auteur: string;
  description: string;
  duree: string;
  date_sortie: string;
  affiche: string;
  seances: Seance[];
};

export const useMovieDetails = (id: number | undefined) => {
  const fetchMovie = async (): Promise<Movie> => {
    const res = await fetch(`${API_URL}/Film/${id}`);
    if (!res.ok) throw new Error("Film introuvable");
    return res.json();
  };

  return useQuery<Movie, Error>({
    queryKey: ["movie", id],
    queryFn: fetchMovie,
    enabled: typeof id === "number" && !isNaN(id),
  });
};

export const useNowPlayingMovies = () => {
  const fetchNowPlaying = async (): Promise<Movie[]> => {
    const res = await fetch(`${API_URL}/Film`);
    if (!res.ok) {
      throw new Error("Erreur lors du chargement des films à l'affiche");
    }
    return res.json();
  };

  return useQuery<Movie[], Error>({
    queryKey: ["nowPlaying"],
    queryFn: fetchNowPlaying,
  });
};

export const useUpcomingMovies = () => {
  const fetchUpcoming = async (): Promise<Movie[]> => {
    const res = await fetch(`${API_URL}/upcomingMovies`);
    if (!res.ok) {
      throw new Error("Erreur lors du chargement des prochaines sorties");
    }
    return res.json();
  };

  return useQuery<Movie[], Error>({
    queryKey: ["upcomingMovies"],
    queryFn: fetchUpcoming,
  });
};
