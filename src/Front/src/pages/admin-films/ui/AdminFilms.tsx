import { FilmPoster } from "../../../components/admin/FilmPoster";
import {useFilms} from "../../../hooks/useFilms";

export const AdminFilms: React.FC = () => {
    const { data: films, isLoading, isError, error } = useFilms();

    if (isLoading) return <p>Chargement des films...</p>;
    if (isError) return <p>{(error as Error).message}</p>;

    return (
        <>
            <p>Entrées totales</p>
            <h1 className="text-6xl">Avril</h1>
            <div className="grid grid-cols-3 gap-x-14 mr-[10vw] mt-10">
                {films?.map((film) => (
                    <FilmPoster
                        key={film.id_film}
                        id={film.id_film}
                        poster={film.affiche}
                        // @ts-ignore
                        entries={Math.floor(Math.random() * 300)}
                    />
                ))}
            </div>

        </>
    );
};
