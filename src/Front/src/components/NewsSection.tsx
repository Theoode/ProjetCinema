import Actualites from "../assets/actualites.png";
import Button from "./Button";

export const NewsSection = () => {
  return (
    <div className="text-white p-6">
      <h2 className="text-3xl font-bold mb-6 text-center md:text-left">
        Actualités
      </h2>
      <div
        className="relative w-full h-60 bg-cover bg-center rounded-lg"
        style={{ backgroundImage: `url(${Actualites})` }}
      ></div>
      <div className="mt-6 flex justify-center">
        <Button className="border-2 border-solid border-primary hover:bg-primary transition duration-300 mt-4">
          Voir toutes les actualités
        </Button>
      </div>
    </div>
  );
};
