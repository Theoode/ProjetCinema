interface MovieCardProps {
  title: string;
  image: string;
}

const MovieCard: React.FC<MovieCardProps> = ({ title, image }) => {
  const fallbackImage =
    "https://via.placeholder.com/300x450?text=Image+indisponible";

  return (
    <div className="w-40 md:w-48">
      <img
        src={image || fallbackImage}
        alt={title}
        className="w-full h-[300px] object-cover rounded-lg"
        onError={(e) => {
          const target = e.target as HTMLImageElement;
          target.src = fallbackImage;
        }}
      />
      <p className="text-white text-center mt-2">{title}</p>
    </div>
  );
};

export default MovieCard;
