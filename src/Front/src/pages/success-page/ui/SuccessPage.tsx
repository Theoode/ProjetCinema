import { useNavigate } from "react-router-dom";
import Success from "../../../assets/success.png";
import Button from "../../../components/Button";

const SuccessPage = () => {
  const navigate = useNavigate();

  return (
    <div className="text-white min-h-screen flex flex-col justify-center items-center px-6">
      <img src={Success} alt="Succès" className="w-32 h-32 mb-6" />
      <h1 className="text-2xl font-bold text-center mb-4">
        Votre réservation a été confirmée !
      </h1>
      <p className="text-gray-400 text-center mb-6">
        Veuillez retrouver votre e-billet dans la section{" "}
        <strong>“Mes réservations”</strong>.
      </p>
      <Button
        className="bg-primary hover:bg-hover px-6 py-3"
        onClick={() => navigate("/")}
      >
        Retourner à l'accueil
      </Button>
    </div>
  );
};

export default SuccessPage;
