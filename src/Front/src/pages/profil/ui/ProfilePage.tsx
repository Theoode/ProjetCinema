import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import Button from "../../../components/Button";
import Card from "../../../components/Card";
import { Footer } from "../../../components/Footer";
import { Navbar } from "../../../components/Navbar";
import Spacing from "../../../components/Spacing";

const ProfilePage = () => {
  const [user, setUser] = useState<{ firstname: string; id: string } | null>(
    null
  );
  const [reservations, setReservations] = useState<any[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const navigate = useNavigate();

  useEffect(() => {
    const storedUser = localStorage.getItem("user");
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    } else {
      navigate("/login");
    }
  }, [navigate]);

  useEffect(() => {
    const stored = JSON.parse(localStorage.getItem("reservations") ?? "[]");
    setReservations(stored);
    setIsLoading(false);
  }, []);

  const handleCancelReservation = (id: string) => {
    const confirmDelete = window.confirm("Supprimer cette réservation ?");
    if (!confirmDelete) return;

    const updated = reservations.filter((r) => r.id !== id);
    localStorage.setItem("reservations", JSON.stringify(updated));
    setReservations(updated);
  };

  return (
    <div className="text-white min-h-screen">
      <Navbar />
      <Spacing size="lg" />
      <div className="max-w-screen-lg mx-auto px-6 py-12 w-full">
        <h1 className="text-3xl font-bold text-center md:text-left">
          Bonjour {user?.firstname}
        </h1>

        <h2 className="text-xl font-semibold mt-6 text-center md:text-left">
          Mes réservations
        </h2>

        {isLoading ? (
          <p className="mt-6 text-gray-400 text-center">Chargement...</p>
        ) : reservations.length > 0 ? (
          <div className="w-full flex flex-col items-center md:items-start space-y-6">
            {reservations.map((res) => (
              <Card
                key={res.id}
                className="w-full max-w-lg md:max-w-none flex flex-col md:flex-row items-center md:items-start p-4 sm:p-6 mt-6 space-y-4 md:space-y-0 md:space-x-6"
              >
                <img
                  src={res.image}
                  alt={res.movieTitle}
                  className="w-full md:w-48 h-auto md:h-32 object-cover rounded-lg self-center md:self-start"
                />

                <div className="flex flex-col items-center md:items-start text-center md:text-left w-full">
                  <h3 className="text-lg font-semibold">{res.movieTitle}</h3>
                  <p className="text-sm text-gray-300">
                    {res.date} à {res.time}
                    <br />
                    {res.room}, {res.seat}
                  </p>

                  <div className="flex flex-col sm:flex-row gap-3 mt-4 w-full sm:w-auto">
                    <Button
                      className="bg-primary hover:bg-hover transition duration-300 w-full sm:w-auto"
                      onClick={() => navigate(`/reservation/${res.id}`)}
                    >
                      Consulter ma réservation
                    </Button>
                    <Button
                      className="border border-red-500 text-red-500 hover:bg-red-500 hover:text-white w-full sm:w-auto"
                      onClick={() => handleCancelReservation(res.id)}
                    >
                      Annuler ma réservation
                    </Button>
                  </div>
                </div>
              </Card>
            ))}
          </div>
        ) : (
          <p className="mt-6 text-gray-400 text-center">
            Aucune réservation pour le moment.
          </p>
        )}
      </div>

      <Spacing size="lg" />
      <Footer />
    </div>
  );
};

export default ProfilePage;
