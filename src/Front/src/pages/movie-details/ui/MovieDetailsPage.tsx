import { FaStar } from "react-icons/fa";
import { Link, useParams } from "react-router-dom";
import Button from "../../../components/Button";
import { Footer } from "../../../components/Footer";
import { Navbar } from "../../../components/Navbar";
import { useMovieDetails } from "../../../hooks/useMoviesProvider";

const MovieDetailsPage = () => {
  const { id } = useParams();
  const movieId = Number(id);
  const { data: movie, isLoading, isError } = useMovieDetails(movieId);

  if (isLoading) return <div className="text-white">Chargement...</div>;
  if (isError || !movie)
    return <div className="text-white">Film introuvable</div>;

  return (
    <div className="text-white min-h-screen bg-background">
      <Navbar />

      {/* Bandeau d'affiche */}
      <div
        className="w-full h-[400px] bg-cover bg-center relative"
        style={{ backgroundImage: `url(${movie.affiche})` }}
      >
        <div className="absolute bottom-0 left-0 w-full h-24 bg-gradient-to-b from-transparent to-background"></div>
      </div>

      {/* Contenu principal */}
      <div className="max-w-screen-lg mx-auto px-6 py-10">
        <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
          <img
            src={movie.affiche}
            alt={movie.nom_film}
            className="hidden md:block rounded-lg w-full"
          />
          <div className="md:col-span-2 flex flex-col justify-center">
            <h1 className="text-4xl font-bold">{movie.nom_film}</h1>
            <div className="flex items-center text-yellow-400 mt-2">
              <FaStar /> <span className="ml-1 text-white">4.5</span>
            </div>
            <p className="text-gray-400 mt-2">
              {movie.duree} - {movie.auteur}
            </p>
            <p className="text-gray-400 mt-2">
              Sortie : {new Date(movie.date_sortie).toLocaleDateString()}
            </p>
            <p className="mt-4">{movie.description}</p>
          </div>
        </div>

        {/* Bandes annonces */}
        <div className="flex flex-col items-center lg:items-start text-center lg:text-left">
          <h2 className="text-2xl font-bold mt-12">Bandes annonces</h2>
          <div className="grid grid-cols-1 md:grid-cols-3 gap-4 mt-4">
            <p className="text-gray-400 col-span-full">
              Aucune bande annonce disponible.
            </p>
          </div>

          {/* Horaires */}
          <h2 className="text-2xl font-bold mt-12">Horaires</h2>
          <div className="flex flex-wrap gap-4 mt-4 justify-center lg:justify-start">
            {movie.seances?.map((seance) => (
              <Link
                key={seance.id_seance}
                to={`/films/${movieId}/seance/${seance.id_seance}`}
              >
                <Button className="border-2 border-solid border-primary hover:bg-primary transition duration-300">
                  {new Date(seance.date_seance).toLocaleString("fr-FR", {
                    weekday: "short",
                    hour: "2-digit",
                    minute: "2-digit",
                    day: "2-digit",
                    month: "short",
                  })}{" "}
                </Button>
              </Link>
            ))}
          </div>
        </div>
      </div>

      <Footer />
    </div>
  );
};

export default MovieDetailsPage;
