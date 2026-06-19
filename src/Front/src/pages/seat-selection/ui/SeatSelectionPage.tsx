import { useState } from "react";
import { FaUser } from "react-icons/fa";
import { useNavigate, useParams } from "react-router-dom";
import Seat from "../../../assets/seat.png";
import SeatSelected from "../../../assets/seatSelected.png";
import Button from "../../../components/Button";
import { Navbar } from "../../../components/Navbar";
import Spacing from "../../../components/Spacing";
import {
  Place,
  useSalleFromSeanceId,
} from "../../../hooks/useSalleFromSeanceId";

const SeatSelectionPage = () => {
  const { seanceId } = useParams();
  const navigate = useNavigate();
  const [selectedSeats, setSelectedSeats] = useState<number[]>([]);

  const {
    data: salle,
    isLoading,
    isError,
  } = useSalleFromSeanceId(Number(seanceId));

  if (isLoading)
    return <div className="text-white">Chargement des places...</div>;
  if (isError || !salle)
    return (
      <div className="text-white">Erreur lors du chargement des places.</div>
    );

  const totalPlaces = salle.capacite;

  const fakePlaces: Place[] = Array.from({ length: totalPlaces }, (_, i) => ({
    id_place: i + 1,
    numero_place: i + 1,
    disponibilite: true,
    fk_salle: salle.id_salle,
    fk_reservation: null,
  }));

  const reservedPlaces = fakePlaces
    .filter((p) => p.fk_reservation !== null)
    .map((p) => p.numero_place);

  const availableCount = totalPlaces - selectedSeats.length;

  // Regrouper les places par ligne de 12
  const cols = 12;
  const grid = [] as Place[][];
  for (let i = 0; i < fakePlaces.length; i += cols) {
    grid.push(fakePlaces.slice(i, i + cols));
  }

  const toggleSeat = (numero_place: number, isReserved: boolean) => {
    if (isReserved) return;

    if (selectedSeats.includes(numero_place)) {
      setSelectedSeats((prev) => prev.filter((id) => id !== numero_place));
    } else {
      setSelectedSeats((prev) => [...prev, numero_place]);
    }
  };

  return (
    <div className="text-white min-h-screen">
      <Navbar />
      <Spacing size="lg" />
      <div className="max-w-screen-lg mx-auto px-6 py-10 text-center">
        <h1 className="text-3xl font-bold">Sélectionner vos places</h1>
        <p className="text-gray-400 mt-2">{availableCount} places libres</p>

        <div className="flex flex-col items-center mt-6 space-y-2">
          {grid.map((row, rowIndex) => (
            <div key={rowIndex} className="flex space-x-2">
              {row.map((place) => {
                const isSelected = selectedSeats.includes(place.numero_place);
                const isReserved = reservedPlaces.includes(place.numero_place);

                return (
                  <img
                    key={place.numero_place}
                    src={
                      isSelected ? SeatSelected : isReserved ? undefined : Seat
                    }
                    alt="seat"
                    className={`w-6 h-6 cursor-pointer rounded ${
                      isReserved
                        ? "bg-gray-500 cursor-not-allowed"
                        : "hover:opacity-80"
                    }`}
                    onClick={() => toggleSeat(place.numero_place, isReserved)}
                  />
                );
              })}
            </div>
          ))}
        </div>

        <div className="mt-6 border border-gray-500 px-60 py-2 inline-block rounded-md">
          Écran
        </div>

        <div className="flex justify-center mt-6 space-x-6 text-sm">
          <div className="flex items-center space-x-2">
            <img src={SeatSelected} alt="seatSelected" className="w-6 h-5" />
            <span>Mes places</span>
          </div>
          <div className="flex items-center space-x-2">
            <img src={Seat} alt="seat" className="w-6 h-5" />
            <span>Places libres</span>
          </div>
          <div className="flex items-center space-x-2">
            <div className="w-5 h-5 bg-gray-500 rounded-md flex items-center justify-center">
              <FaUser className="text-white text-xs" />
            </div>
            <span>Places occupées</span>
          </div>
        </div>

        <div className="flex justify-between mt-6">
          <Button
            className="border-2 border-primary text-white px-6 py-2 hover:bg-primary transition duration-300"
            onClick={() => navigate(-1)}
          >
            Retour
          </Button>
          <Button
            className={`px-6 py-2 transition duration-300 ${
              selectedSeats.length === 0
                ? "bg-gray-500 cursor-not-allowed"
                : "bg-primary hover:bg-hover"
            }`}
            onClick={() => {
              localStorage.setItem(
                "selectedSeatsCount",
                selectedSeats.length.toString()
              );
              localStorage.setItem("seanceId", String(seanceId));
              navigate("/tarif");
            }}
          >
            Réserver votre place
          </Button>
        </div>
      </div>
    </div>
  );
};

export default SeatSelectionPage;
