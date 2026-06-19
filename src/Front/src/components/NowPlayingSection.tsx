import "swiper/css";
import "swiper/css/navigation";
import "swiper/css/pagination";
import { Navigation, Pagination } from "swiper/modules";
import { Swiper, SwiperSlide } from "swiper/react";
import { useNowPlayingMovies } from "../hooks/useNowPlayingMovies";

import { Link } from "react-router-dom";
import Button from "./Button";
import MovieCard from "./MovieCard";

const NowPlayingSection = () => {
  const { data: movies, isLoading, isError } = useNowPlayingMovies();

  if (isLoading) {
    return (
      <section className="text-white px-6 py-10 max-w-screen-2xl mx-auto w-full">
        <h2 className="text-3xl font-bold mb-6 text-center md:text-left">
          Films à l'affiche
        </h2>
        <p className="text-center text-gray-400">Chargement des films...</p>
      </section>
    );
  }

  if (isError || !movies) {
    return (
      <section className="text-white px-6 py-10 max-w-screen-2xl mx-auto w-full">
        <h2 className="text-3xl font-bold mb-6 text-center md:text-left">
          Films à l'affiche
        </h2>
        <p className="text-center text-red-400">
          Une erreur est survenue lors du chargement des films.
        </p>
      </section>
    );
  }

  return (
    <section className="text-white px-6 py-10 max-w-screen-2xl mx-auto w-full">
      <h2 className="text-3xl font-bold mb-6 text-center md:text-left">
        Films à l'affiche
      </h2>

      <div className="relative">
        <Swiper
          spaceBetween={1}
          navigation={true}
          pagination={{ clickable: true }}
          modules={[Navigation, Pagination]}
          breakpoints={{
            1536: { slidesPerView: 5 },
            1280: { slidesPerView: 4 },
            1024: { slidesPerView: 3 },
            768: { slidesPerView: 2 },
            480: { slidesPerView: 1 },
          }}
        >
          {movies.map((movie) => (
            <SwiperSlide key={movie.id_film} className="flex justify-center">
              <MovieCard title={movie.nom_film} image={movie.affiche} />
            </SwiperSlide>
          ))}
        </Swiper>
      </div>

      <div className="flex justify-center mt-6">
        <Link to="/films-a-l-affiche">
          <Button className="border-2 border-primary text-white px-6 py-2 hover:bg-primary transition duration-300">
            Voir tous les films à l'affiche
          </Button>
        </Link>
      </div>
    </section>
  );
};

export default NowPlayingSection;
