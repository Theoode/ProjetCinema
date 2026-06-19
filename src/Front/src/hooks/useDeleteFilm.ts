import { useState } from "react";

export function useDeleteFilm() {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [success, setSuccess] = useState(false);

    const deleteFilm = async (id: number) => {
        setLoading(true);
        setError(null);
        setSuccess(false);

        try {
            const response = await fetch(`http://35.181.160.232:5000/api/Film/${id}`, {
                method: "DELETE",
            });

            if (!response.ok) {
                const errorText = await response.text();
                throw new Error(errorText || "Erreur lors de la suppression du film");
            }

            setSuccess(true);
        } catch (err: any) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    };

    return { deleteFilm, loading, error, success };
}