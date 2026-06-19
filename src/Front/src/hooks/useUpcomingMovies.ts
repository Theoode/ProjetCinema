import { useQuery } from "@tanstack/react-query";

export type Movie = {
  id: number;
  title: string;
  image: string;
};

export const useUpcomingMovies = () => {
  const fetchUpcoming = async (): Promise<Movie[]> => {
    const res = await fetch("http://35.181.160.232:5000/upcomingMovies");
    if (!res.ok) throw new Error("Erreur lors du chargement des films à venir");
    return res.json();
  };

  return useQuery<Movie[], Error>({
    queryKey: ["upcomingMovies"],
    queryFn: fetchUpcoming,
  });
};
