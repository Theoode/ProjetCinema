import { useEffect, useState } from "react";

interface Film {
  id_film: number;
  nom_film: string;
  affiche: string;
  auteur: string;
  description: string;
  duree: string;
  date_sortie: string;
  faitPartie: any[];
  seances: any;
}

export const useFilmID = (filmId: number) => {
  const [film, setFilm] = useState<Film | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (filmId === -1 || filmId == null) {
      setLoading(false);
      setFilm(null);
      return;
    }

    const fetchFilm = async () => {
      try {
        setLoading(true);
        const res = await fetch(
          `http://35.181.160.232:5000/api/Film/${filmId}`
        );

        if (!res.ok) {
          throw new Error(`Erreur HTTP : ${res.status} ${res.statusText}`);
        }

        const data = await res.json();

        if (!data || !data.nom_film || !data.affiche) {
          throw new Error("Structure de film inattendue ou incomplète");
        }

        setFilm(data);
        setError(null);
      } catch (err: any) {
        console.error("Erreur attrapée lors du fetch :", err);
        setError(`Erreur de récupération du film : ${err.message}`);
      } finally {
        setLoading(false);
      }
    };

    fetchFilm();
  }, [filmId]);

  return { film, loading, error };
};
