import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useDeleteFilm } from "../../hooks/useDeleteFilm";

interface FilmPosterProps {
    poster: string;
    entries: string;
    id: number;
    title: string;
}

export const FilmPoster: React.FC<FilmPosterProps> = ({ poster, entries, id, title }) => {
    const [isModalOpen, setIsModalOpen] = useState(false);
    const navigate = useNavigate();

    const { deleteFilm, loading, error, success } = useDeleteFilm();

    const handleDelete = async () => {
        await deleteFilm(id);
    };

    useEffect(() => {
        if (success) {
            navigate("/admin/gestion-films");
        }
    }, [success, navigate]);

    return (
        <div className="text-center mt-10">
            <div className="relative group">
                <img
                    src={`${poster}`}
                    alt="Affiche du film"
                    className="rounded-2xl w-full h-full object-cover cursor-pointer group-hover:blur-sm transition duration-300"
                />

                <div className="absolute inset-0 flex items-center justify-center opacity-0 group-hover:opacity-100 transition duration-300">
                    <div className="flex flex-col gap-2">
                        <button
                            onClick={() => setIsModalOpen(true)}
                            className="bg-[rgba(238,174,74,1)] text-white px-10 py-6 mt-4 shadow-[0_0_20px_3px_rgba(238,174,74,1)] rounded-xl transition scale-100 hover:scale-105 duration-1000"
                        >
                            <h3>Supprimer</h3>
                        </button>
                    </div>
                </div>
            </div>
            <p className="text-sm mt-1">{entries}</p>

            {isModalOpen && (
                <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50">
                    <div className="bg-[#161616] p-[50px] px-[120px] shadow-[0_0_20px_3px_rgba(238,174,74,1)] rounded-[25px] text-center">
                        <p className="mb-2">Souhaitez-vous supprimer le film suivant ?</p>
                        <h1 className="mb-[30px] text-6xl">{title}</h1>

                        {error && <p className="text-red-500 mb-4">{error}</p>}
                        {loading && <p className="text-yellow-300 mb-4">Suppression en cours...</p>}

                        <div className="flex justify-center gap-8">
                            <button
                                onClick={handleDelete}
                                disabled={loading}
                                className="bg-[#eeae4a] button text-white px-10 py-4 rounded-xl transition scale-100 hover:scale-105 duration-1000 disabled:opacity-50"
                            >
                                Supprimer
                            </button>
                            <button
                                onClick={() => setIsModalOpen(false)}
                                disabled={loading}
                                className="bg-[#161616] button text-white px-10 py-4 rounded-xl shadow-[0_0_20px_3px_rgba(238,174,74,1)] transition scale-100 hover:scale-105 duration-1000"
                            >
                                Annuler
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};