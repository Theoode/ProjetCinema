import { Link } from "react-router-dom";
interface FilmPosterProps {
    poster: string;
    entries: string;
    id: number;
}

export const FilmPoster: React.FC<FilmPosterProps> = ({ poster, entries, id }) => {
    return (
        <div className="text-center w-auto h-auto mt-10">
            <Link to={`../film/${id}`} className="block">
                <img
                    src={`${poster}`}
                    alt="Affiche du film"
                    className="rounded-2xl w-full h-full object-cover cursor-pointer"
                />
            </Link>
            <p className="text-sm mt-1">{entries}</p>
        </div>
    );
};