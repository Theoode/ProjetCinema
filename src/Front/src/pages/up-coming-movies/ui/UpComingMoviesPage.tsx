import { Footer } from "../../../components/Footer";
import MovieCard from "../../../components/MovieCard";
import { Navbar } from "../../../components/Navbar";
import Spacing from "../../../components/Spacing";
import { useUpcomingMovies } from "../../../hooks/useUpcomingMovies";

const UpComingMoviesPage = () => {
  const { data: movies, isLoading, error } = useUpcomingMovies();

  return (
    <div className="text-white min-h-screen bg-background">
      <Navbar />
      <Spacing size="lg" />
      <div className="max-w-screen-2xl mx-auto px-6 py-10 text-center md:text-left">
        <h2 className="text-3xl font-bold mb-10">Prochaines sorties</h2>

        {isLoading && <p>Chargement...</p>}
        {error && <p className="text-red-500">Erreur de chargement</p>}

        <div className="flex justify-center">
          <div className="grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-5 gap-6 w-full max-w-screen-xl px-4">
            {movies?.map((movie) => (
              <MovieCard
                key={movie.id}
                title={movie.title}
                image={movie.image}
              />
            ))}
          </div>
        </div>
      </div>
      <Footer />
    </div>
  );
};

export default UpComingMoviesPage;
