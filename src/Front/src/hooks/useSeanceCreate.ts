import { useMutation, useQueryClient } from "@tanstack/react-query";

export type NouvelleSeancePayload = {
    date_seance: string;
    fk_film: number;
    fk_salle: number;
};

export const useSeanceCreate = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: async (nouvelleSeance: { date_seance: string; fk_film: number; fk_salle: number }) => {
            const res = await fetch("http://35.181.160.232:5000/api/Seance", {
                method: "POST",
                headers: {
                    "Content-Type": "application/json",
                },
                body: JSON.stringify(nouvelleSeance),
            });

            if (!res.ok) {
                const errorMessage = await res.text(); // ici on lit le texte brut
                throw new Error(errorMessage || "Erreur lors de l'ajout de la séance");
            }

            // ✅ on vérifie si le body n’est pas vide avant de parser
            const contentType = res.headers.get("content-type");
            if (contentType && contentType.includes("application/json")) {
                return res.json();
            }

            // Si le backend renvoie rien
            return null;
        },
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["seances"] });
        },
        onError: (error: Error) => {
            console.error("Erreur détaillée :", error.message);
        },
    });
};