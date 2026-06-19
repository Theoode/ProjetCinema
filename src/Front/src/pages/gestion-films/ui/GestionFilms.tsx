import { Link } from "react-router-dom";
import { FilmPoster } from "../../../components/admin/GestionFilmPoster";
import { useFilms } from "../../../hooks/useFilms"; // <-- adapte le chemin si besoin

export const GestionFilms: React.FC = () => {
    const { data: films, isLoading, isError } = useFilms();

    if (isLoading) return <p>Chargement des films...</p>;
    if (isError) return <p>Erreur lors du chargement des films.</p>;


    return (
        <>
            <p>Gestion</p>
            <h1 className="text-6xl">Films</h1>
            <div className="grid grid-cols-3 gap-x-14 mr-[10vw] mt-10">
                <Link
                    to="/admin/new-film"
                    className="relative w-full h-[80%] mt-10 rounded-2xl bg-[#161616] shadow-[0_0_20px_3px_rgba(238,174,74,1)] flex items-center justify-center hover:opacity-80 transition"
                >
                    <span className="text-[5rem] text-[rgba(238,174,74,1)]">+</span>
                </Link>

                {films?.map((film, index) => (
                    <FilmPoster
                        key={film.id_film}
                        id={film.id_film}
                        title={film.nom_film}
                        // @ts-ignore
                        entries={Math.floor(Math.random() * 300)}
                        poster={film.affiche}
                    />
                ))}
            </div>
        </>
    );
};