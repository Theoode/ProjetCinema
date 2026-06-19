import { useEffect, useState } from "react";
import { FaMinus, FaPlus } from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import Button from "../../../components/Button";
import Card from "../../../components/Card";
import { Navbar } from "../../../components/Navbar";
import Spacing from "../../../components/Spacing";
import { useFilmID } from "../../../hooks/useFilmID";
import { useSalleFromSeanceId } from "../../../hooks/useSalleFromSeanceId";
import { useSeanceById } from "../../../hooks/useSeanceById";
import { useTarifs } from "../../../hooks/useTarifs";

const TarifsPage = () => {
  const navigate = useNavigate();
  const [maxSeats, setMaxSeats] = useState<number>(0);
  const [tarifsSelection, setTarifsSelection] = useState<
    Record<number, number>
  >({});

  const seanceId = Number(localStorage.getItem("seanceId"));
  const selectedSeatsCount = Number(localStorage.getItem("selectedSeatsCount"));

  const { data: seance, isLoading: seanceLoading } = useSeanceById(seanceId);
  useSalleFromSeanceId(seanceId);
  const { film, loading: filmLoading } = useFilmID(seance?.fk_film ?? 0);
  const { data: tarifs, isLoading: tarifsLoading, isError } = useTarifs();

  useEffect(() => {
    setMaxSeats(selectedSeatsCount);
  }, [selectedSeatsCount]);

  useEffect(() => {
    if (tarifs) {
      const initial = Object.fromEntries(tarifs.map((t) => [t.id_tarif, 0]));
      setTarifsSelection(initial);
    }
  }, [tarifs]);

  const totalSelected = Object.values(tarifsSelection).reduce(
    (sum, val) => sum + val,
    0
  );

  const handleTarifChange = (id: number, action: "increase" | "decrease") => {
    setTarifsSelection((prev) => {
      const currentCount = prev[id] || 0;
      const newCount =
        action === "increase"
          ? totalSelected < maxSeats
            ? currentCount + 1
            : currentCount
          : Math.max(currentCount - 1, 0);
      return { ...prev, [id]: newCount };
    });
  };

  const total =
    tarifs?.reduce(
      (sum, tarif) =>
        sum + (tarifsSelection[tarif.id_tarif] || 0) * tarif.valeur,
      0
    ) ?? 0;

  const handleContinuer = () => {
    if (film) {
      localStorage.setItem("filmNom", film.nom_film);
      localStorage.setItem("filmAffiche", film.affiche);
    }
    navigate("/confiseries");
  };

  return (
    <div className="text-white min-h-screen">
      <Navbar />
      <Spacing size="lg" />
      <div className="max-w-screen-lg mx-auto px-6 py-10 text-center">
        <h1 className="text-3xl font-bold mb-8">Sélectionner vos tarifs</h1>
        <p className="mb-4 text-gray-400">
          {totalSelected} / {maxSeats} places sélectionnées
        </p>

        <Card className="w-full max-w-3xl mx-auto p-6 sm:p-8">
          <div className="flex flex-col md:flex-row items-center md:items-start md:space-x-6 mb-6">
            {filmLoading || seanceLoading ? (
              <p className="text-white">Chargement du film...</p>
            ) : film && seance ? (
              <>
                <img
                  src={film.affiche}
                  alt={film.nom_film}
                  className="w-32 h-auto md:w-24 rounded-lg"
                />
                <div className="text-center md:text-left mt-4 md:mt-0">
                  <h2 className="text-lg sm:text-xl font-bold">
                    {film.nom_film}
                  </h2>
                  <p className="text-gray-400 text-sm sm:text-base">
                    {new Date(seance.date_seance).toLocaleString("fr-FR", {
                      dateStyle: "long",
                      timeStyle: "short",
                    })}
                  </p>
                  <p className="text-gray-400 text-sm sm:text-base">
                    Place(s) : {totalSelected}
                  </p>
                  <p className="text-gray-400 text-sm sm:text-base">
                    Tarif : {total}€
                  </p>
                </div>
              </>
            ) : (
              <p className="text-red-500">Film ou séance introuvable</p>
            )}
          </div>

          {tarifsLoading ? (
            <p className="text-white">Chargement des tarifs...</p>
          ) : isError || !tarifs ? (
            <p className="text-red-500">
              Erreur lors du chargement des tarifs.
            </p>
          ) : (
            <div className="space-y-6">
              {tarifs.map((tarif) => (
                <div
                  key={tarif.id_tarif}
                  className="flex items-center justify-between border-b border-gray-500 pb-2 text-sm sm:text-base"
                >
                  <div className="flex items-center space-x-4">
                    <span className="font-bold">{tarif.nom_tarif}</span>
                    <span className="text-gray-400">-</span>
                    <span className="text-gray-400">{tarif.valeur}€</span>
                  </div>
                  <div className="flex items-center space-x-2">
                    <button
                      className="p-2 rounded-full bg-gray-700 hover:bg-gray-600 transition"
                      onClick={() =>
                        handleTarifChange(tarif.id_tarif, "decrease")
                      }
                      disabled={tarifsSelection[tarif.id_tarif] === 0}
                    >
                      <FaMinus className="text-white" />
                    </button>
                    <span className="text-lg">
                      {tarifsSelection[tarif.id_tarif] || 0}
                    </span>
                    <button
                      className="p-2 rounded-full bg-primary hover:bg-hover transition"
                      onClick={() =>
                        handleTarifChange(tarif.id_tarif, "increase")
                      }
                      disabled={totalSelected >= maxSeats}
                    >
                      <FaPlus className="text-white" />
                    </button>
                  </div>
                </div>
              ))}
            </div>
          )}

          <div className="mt-6">
            <Button
              className={`w-full py-3 mt-4 ${
                total === 0
                  ? "bg-gray-500 cursor-not-allowed"
                  : "bg-primary hover:bg-hover"
              }`}
              disabled={total === 0}
              onClick={handleContinuer}
            >
              Continuer
            </Button>
          </div>
        </Card>
      </div>
    </div>
  );
};

export default TarifsPage;
