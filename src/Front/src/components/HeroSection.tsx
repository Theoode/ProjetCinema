import Affiche from "../assets/affiche.png";
import Ticket from "../assets/ticket.png";
import Button from "./Button";

export const HeroSection = () => {
  return (
    <div
      className="relative w-full h-[500px] bg-cover bg-center"
      style={{ backgroundImage: `url(${Affiche})` }}
    >
      <div className="absolute inset-0 bg-black bg-opacity-50 flex flex-col justify-center items-center text-white text-center px-6">
        <h1 className="text-5xl font-bold">WICKED</h1>
        <Button className="bg-primary hover:bg-hover mt-4 flex items-center space-x-2 px-6 py-3">
          <span>
            <img src={Ticket} alt="ticket" />
          </span>
          <span>Réserver dès maintenant</span>
        </Button>
      </div>
      <div className="absolute bottom-0 left-0 w-full h-24 bg-gradient-to-b from-transparent to-background"></div>
    </div>
  );
};
