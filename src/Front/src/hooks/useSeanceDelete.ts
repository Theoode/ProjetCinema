import { useMutation, useQueryClient } from "@tanstack/react-query";

export const useSeanceDelete = () => {
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: async (seanceId: number) => {
            const res = await fetch(`http://35.181.160.232:5000/api/Seance/${seanceId}`, {
                method: "DELETE",
            });
            if (!res.ok) throw new Error("Erreur lors de la suppression de la séance");
            return res.json();
        },
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ["seances"] });
        },
    });
};
