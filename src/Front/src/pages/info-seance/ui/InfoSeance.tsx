import {useState, useEffect} from "react";
import {Link, useParams} from "react-router-dom";
import {useFilmID} from "../../../hooks/useFilmID";

export const InfoSeance = () => {
    const {id} = useParams<{ id?: string }>();
    const [seance, setSeance] = useState<any | null>(null);
    const [loadingSeance, setLoadingSeance] = useState(true);
    const [errorSeance, setErrorSeance] = useState<string | null>(null);

    const filmId = seance?.fk_film ?? null;
    const {
        film,
        loading: loadingFilm,
        error: errorFilm,
    } = useFilmID(filmId ?? -1);

    useEffect(() => {
        const fetchSeance = async () => {
            try {
                const res = await fetch(`http://35.181.160.232:5000/api/Seance/${id}`);
                if (!res.ok) throw new Error(`Erreur HTTP : ${res.status} ${res.statusText}`);
                const data = await res.json();
                setSeance(data);
            } catch (err: any) {
                setErrorSeance(err.message);
            } finally {
                setLoadingSeance(false);
            }
        };

        if (id) fetchSeance();
        else {
            setErrorSeance("Aucune ID de séance trouvée.");
            setLoadingSeance(false);
        }
    }, [id]);

    if (loadingSeance) return <p>Chargement de la séance...</p>;
    if (errorSeance || !seance) return <p>{errorSeance ?? "Séance introuvable."}</p>;

    if (loadingFilm) return <p>Chargement du film...</p>;
    if (errorFilm || !film) return <p>{errorFilm ?? "Film introuvable."}</p>;

    const dateSeance = new Date(seance.date_seance);

    const handleDelete = async () => {
        if (id) {
            const response = await fetch(`http://35.181.160.232:5000/api/Seance/${id}`, {
                method: "DELETE",
                credentials: "include",
            });

            if (response.ok) {
                alert("Séance supprimée avec succès !");
                window.location.href = "/admin/seance";
            } else {
                alert("Erreur lors de la suppression.");
            }
        }
    };

    return (
        <div className="relative">
            <p>Informations</p>
            <h1 className="text-6xl mb-6">Séance</h1>
            <Link
                to="/admin/seance"
                className="absolute top-0 right-8 bg-[#EEAE4A] text-white px-6 py-3 rounded-lg shadow-[0_0_20px_3px_rgba(238,174,74,1)] hover:bg-[#EEAF5A] transition scale-100 hover:scale-105 duration-1000"
            >
                Retour
            </Link>
            <div className="flex gap-10">
                <img
                    src={film.affiche}
                    alt={`Affiche de ${film.nom_film}`}
                    className="rounded-2xl w-1/3 object-cover"
                />
                <div>
                    <h2 className="text-4xl font-bold mb-4">{film.nom_film}</h2>
                    <p><strong>Date
                        :</strong> {dateSeance.toLocaleDateString("fr-FR")} à {dateSeance.toLocaleTimeString("fr-FR", {
                        hour: "2-digit",
                        minute: "2-digit"
                    })}</p>
                    <p><strong>Salle :</strong> {seance.fk_salle}</p>
                    <div className="mt-20">
                        <button
                            onClick={handleDelete}
                            className="text-white px-6 py-3 rounded-lg shadow-[0_0_20px_3px_rgba(238,174,74,1)] hover:bg-red-700 transition scale-100 hover:scale-105 duration-1000"
                        >
                            Supprimer la séance
                        </button>
                    </div>
                </div>

            </div>
        </div>
    );
};