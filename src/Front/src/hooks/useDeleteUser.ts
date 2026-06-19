import { useState } from "react";

export const useDeleteUser = () => {
    const [isDeleting, setIsDeleting] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const deleteUser = async (email: string, fetchUsers: () => void) => {
        setIsDeleting(true);
        setError(null);

        try {
            const res = await fetch(`http://http://35.181.160.232:5000/api/User/${email}`, {
                method: "DELETE",
                headers: {
                    Accept: "application/json",
                    // Ajouter Authorization si nécessaire :
                    // Authorization: `Bearer ${token}`,
                },
            });

            if (!res.ok) {
                throw new Error("Erreur lors de la suppression");
            }

            // Recharger la liste des utilisateurs après la suppression
            fetchUsers();
        } catch (err: any) {
            setError(err.message || "Une erreur est survenue lors de la suppression.");
        } finally {
            setIsDeleting(false);
        }
    };

    return { deleteUser, isDeleting, error };
};
