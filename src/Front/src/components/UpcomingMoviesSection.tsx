import { Link } from "react-router-dom";
import { Navigation, Pagination } from "swiper/modules";
import { Swiper, SwiperSlide } from "swiper/react";
import Vaiana2 from "../assets/vaiana2.png";
import Button from "./Button";
import MovieCard from "./MovieCard";

const upcomingMovies = [
  { title: "Les femmes au balcon", image: Vaiana2 },
  { title: "Vingt dieux", image: Vaiana2 },
  { title: "Jamais sans mon psy", image: Vaiana2 },
  { title: "Le Noël de Teddy l’ourson", image: Vaiana2 },
  { title: "Saint-Ex", image: Vaiana2 },
  { title: "Les femmes au balcon", image: Vaiana2 },
  { title: "Vingt dieux", image: Vaiana2 },
  { title: "Jamais sans mon psy", image: Vaiana2 },
  { title: "Le Noël de Teddy l’ourson", image: Vaiana2 },
  { title: "Saint-Ex", image: Vaiana2 },
];

const UpcomingMoviesSection = () => {
  return (
    <section className="text-white px-6 py-10 max-w-screen-2xl mx-auto w-full">
      <h2 className="text-3xl font-bold mb-6 text-center md:text-left">
        Prochaines sorties
      </h2>

      <div className="relative">
        <Swiper
          spaceBetween={1}
          navigation={true}
          pagination={{ clickable: true }}
          modules={[Navigation, Pagination]}
          breakpoints={{
            1536: { slidesPerView: 5 }, // Très grands écrans
            1280: { slidesPerView: 4 }, // Desktop large
            1024: { slidesPerView: 3 }, // Desktop standard
            768: { slidesPerView: 2 }, // Tablette
            480: { slidesPerView: 1 }, // Mobile
          }}
        >
          {upcomingMovies.map((upcomingMovies, index) => (
            <SwiperSlide key={index} className="flex justify-center">
              <MovieCard
                title={upcomingMovies.title}
                image={upcomingMovies.image}
              />
            </SwiperSlide>
          ))}
        </Swiper>
      </div>

      <div className="flex justify-center mt-6">
        <Link to="/prochaines-sorties">
          <Button className="border-2 border-primary text-white px-6 py-2 hover:bg-primary transition duration-300">
            Voir toutes les prochaines sorties
          </Button>
        </Link>
      </div>
    </section>
  );
};

export default UpcomingMoviesSection;
