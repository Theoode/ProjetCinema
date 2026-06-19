import { Link } from "react-router-dom";
import { Footer } from "../../../components/Footer";
import MovieCard from "../../../components/MovieCard";
import { Navbar } from "../../../components/Navbar";
import Spacing from "../../../components/Spacing";
import { useFilms } from "../../../hooks/useFilms";

const NowPlayingPage = () => {
  const { data: movies, isLoading, isError } = useFilms();

  if (isLoading) return <p className="text-white text-center">Chargement...</p>;
  if (isError) return <p className="text-red-500 text-center">Erreur...</p>;

  return (
    <div className="text-white min-h-screen bg-background">
      <Navbar />
      <Spacing size="lg" />
      <div className="max-w-screen-2xl mx-auto px-6 py-10">
        <h2 className="text-3xl font-bold mb-10 text-center md:text-left">
          Films à l'affiche
        </h2>
        <div className="flex justify-center">
          <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 gap-6 w-full max-w-screen-xl px-4">
            {movies?.map((movie) => (
              <Link key={movie.id_film} to={`/films/${movie.id_film}`}>
                <MovieCard title={movie.nom_film} image={movie.affiche} />
              </Link>
            ))}
          </div>
        </div>
      </div>
      <Footer />
    </div>
  );
};

export default NowPlayingPage;
