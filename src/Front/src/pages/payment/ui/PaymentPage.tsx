import { useState } from "react";
import { useNavigate } from "react-router-dom";
import Button from "../../../components/Button";
import Card from "../../../components/Card";
import Input from "../../../components/Input";
import { Navbar } from "../../../components/Navbar";
import Spacing from "../../../components/Spacing";
import { usePaiement } from "../../../hooks/usePaiement";

const PaymentPage = () => {
  const [cardNumber, setCardNumber] = useState("");
  const [expiry, setExpiry] = useState("");
  const [cvv, setCvv] = useState("");
  const navigate = useNavigate();

  const montant = Number(localStorage.getItem("montant"));
  const filmNom = localStorage.getItem("filmNom") ?? "Film inconnu";
  const filmAffiche = localStorage.getItem("filmAffiche") ?? "";

  const { mutate: payer, isPending } = usePaiement();

  const handlePaiement = () => {
    payer(
      {
        reservationId: 1,
        paiement: {
          montant,
          methode: "CB",
          date_paiement: new Date().toISOString(),
        },
      },
      {
        onSuccess: () => {
          const existing = JSON.parse(
            localStorage.getItem("reservations") ?? "[]"
          );

          const newReservation = {
            id: crypto.randomUUID(),
            movieTitle: filmNom,
            image: filmAffiche,
            date: new Date().toLocaleDateString("fr-FR"),
            time: new Date().toLocaleTimeString("fr-FR", {
              hour: "2-digit",
              minute: "2-digit",
            }),
            room: "Salle 1",
            seat: "A1, A2",
          };

          localStorage.setItem(
            "reservations",
            JSON.stringify([newReservation, ...existing])
          );

          navigate("/success");
        },
      }
    );
  };

  return (
    <div className="text-white min-h-screen">
      <Navbar />
      <Spacing size="lg" />
      <div className="max-w-screen-md mx-auto px-6 py-10 text-center">
        <h1 className="text-3xl font-bold mb-6">Paiement</h1>

        <Card className="w-full max-w-lg mx-auto mb-8">
          <h2 className="text-lg font-bold mb-4">Résumé de la réservation</h2>
          {filmAffiche && (
            <img
              src={filmAffiche}
              alt="Affiche du film"
              className="w-24 mx-auto mb-4 rounded"
            />
          )}
          <p className="text-xl font-semibold mb-2">{filmNom}</p>
          <p className="font-semibold">Montant total : {montant} €</p>
        </Card>

        <Card className="w-full max-w-lg mx-auto">
          <h2 className="text-lg font-bold mb-4">Carte bancaire</h2>
          <Input
            type="text"
            placeholder="Numéro de carte"
            value={cardNumber}
            onChange={(e) => setCardNumber(e.target.value)}
          />
          <div className="flex space-x-4">
            <Input
              type="text"
              placeholder="MM/YY"
              value={expiry}
              onChange={(e) => setExpiry(e.target.value)}
            />
            <Input
              type="text"
              placeholder="CVV"
              value={cvv}
              onChange={(e) => setCvv(e.target.value)}
            />
          </div>

          <Button
            className={`w-full mt-6 ${
              isPending
                ? "bg-gray-500 cursor-not-allowed"
                : "bg-primary hover:bg-hover"
            }`}
            disabled={isPending}
            onClick={handlePaiement}
          >
            {isPending ? "Paiement en cours..." : "Procéder au paiement"}
          </Button>
        </Card>
      </div>
    </div>
  );
};

export default PaymentPage;
