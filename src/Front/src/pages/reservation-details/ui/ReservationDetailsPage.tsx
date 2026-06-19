// @ts-ignore
import html2pdf from "html2pdf.js";
import { useEffect, useRef, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import QRCode from "../../../assets/qrcode.png";
import Button from "../../../components/Button";
import Card from "../../../components/Card";
import { Footer } from "../../../components/Footer";
import { Navbar } from "../../../components/Navbar";
import Spacing from "../../../components/Spacing";
import { useReservationDetails } from "../../../hooks/useReservations";

const ReservationDetailsPage = () => {
  const navigate = useNavigate();
  const { id } = useParams();
  const pdfRef = useRef<HTMLDivElement>(null);

  const [, setUserId] = useState<string | null>(null);

  useEffect(() => {
    const storedUser = localStorage.getItem("user");
    if (storedUser) {
      const parsedUser = JSON.parse(storedUser);
      setUserId(parsedUser.id);
    } else {
      navigate("/login");
    }
  }, [navigate]);

  const reservation = useReservationDetails(id);

  const handleDownloadPDF = () => {
    if (pdfRef.current) {
      const opt = {
        margin: 0,
        filename: "reservation.pdf",
        image: { type: "jpeg", quality: 0.98 },
        html2canvas: { scale: 2 },
        jsPDF: { unit: "in", format: "letter", orientation: "portrait" },
      };

      html2pdf().set(opt).from(pdfRef.current).save();
    }
  };

  if (!reservation) {
    return <p className="text-white text-center">Réservation introuvable</p>;
  }

  return (
    <div className="text-white min-h-screen flex flex-col">
      <Navbar />
      <Spacing size="lg" />
      <div className="flex-grow w-full max-w-screen-xl mx-auto px-6 py-10">
        <h1 className="text-2xl font-bold mb-6 text-center md:text-left">
          Ma réservation
        </h1>
        <div ref={pdfRef}>
          <Card className="w-full max-w-4xl mx-auto p-6 flex flex-col items-center text-center">
            <img
              src={reservation.image}
              alt={reservation.movieTitle}
              className="w-full h-auto rounded-md mb-6 object-cover max-h-[300px]"
            />

            <h2 className="text-xl font-semibold">{reservation.movieTitle}</h2>
            <p className="text-sm text-gray-300 mt-1">
              {reservation.room}, {reservation.seat}
            </p>

            <img
              src={QRCode}
              alt="QR Code"
              className="w-32 h-32 my-6 object-contain"
            />
            <p className="text-sm text-gray-300 mb-4">étudiant – 8€</p>

            <div className="flex justify-between w-full text-sm text-gray-300">
              <span className="font-bold">{reservation.date}</span>
              <span className="font-bold">{reservation.time}</span>
            </div>
          </Card>
        </div>
        <Spacing size="sm" />
        <div className="mt-6 flex flex-col md:flex-row justify-center md:space-x-4 space-y-4 md:space-y-0">
          <Button className="bg-primary hover:bg-hover transition duration-300">
            Transférer
          </Button>
          <Button
            className="border-2 border-solid border-primary hover:bg-primary transition duration-300"
            onClick={handleDownloadPDF}
          >
            Télécharger en PDF
          </Button>
        </div>
      </div>

      <Spacing size="lg" />
      <Footer />
    </div>
  );
};

export default ReservationDetailsPage;
