import { useUsers } from "../../../hooks/useAllUsers";
import { useDeleteUser } from "../../../hooks/useDeleteUser";

export const Clients: React.FC = () => {
    const { users, loading, error, fetchUsers } = useUsers();
    const { deleteUser, isDeleting, error: deleteError } = useDeleteUser();

    return (
        <div>
            <p>Informations</p>
            <h1 className="text-6xl">Utilisateurs</h1>

            {loading && <p>Chargement...</p>}
            {error && <p className="text-red-500">{error}</p>}

            {deleteError && <p className="text-red-500">{deleteError}</p>}

            <div className="mt-10 space-y-8">
                <div className="grid grid-cols-4 text-sm text-gray-400 uppercase">
                    <p>Email</p>
                    <p>Nom</p>
                    <p>Supprimer</p>
                </div>

                {users.map((user, index) => (
                    <div
                        key={index}
                        className="grid grid-cols-4 mt-10 items-center bg-[#161616] text-white p-6 rounded-[22px] shadow-[0_0_20px_3px_rgba(238,174,74,1)]"
                    >
                        <p>{user.email}</p>
                        <p>{user.userName}</p>
                        <button
                            onClick={() => deleteUser(user.email, fetchUsers)}
                            className="text-red-500 hover:text-red-700 transition duration-150 ease-in-out"
                            disabled={isDeleting}
                        >
                            {isDeleting ? "Suppression..." : "x"}
                        </button>
                    </div>
                ))}
            </div>
        </div>
    );
};
