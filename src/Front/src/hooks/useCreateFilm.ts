import { useState } from "react";

export function useCreateFilm() {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [success, setSuccess] = useState(false);

    const createFilm = async (data: {
        nom_film: string;
        auteur: string;
        duree: string;
        date_sortie: string;
        description: string;
        affiche: string;
    }) => {
        setLoading(true);
        setError(null);
        setSuccess(false);

        try {
            const response = await fetch("http://35.181.160.232:5000/api/Film", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify({
                    nom_film: data.nom_film,
                    auteur: data.auteur,
                    duree: data.duree,
                    date_sortie: data.date_sortie,
                    description: data.description,
                    affiche: data.affiche,
                }),
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(errorText || "Erreur lors de la création du film");
            }

            setSuccess(true);
        } catch (err: any) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    return { createFilm, loading, error, success };
}