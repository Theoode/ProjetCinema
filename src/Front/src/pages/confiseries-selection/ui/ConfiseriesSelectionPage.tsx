import { useState } from "react";
import { FaMinus, FaPlus } from "react-icons/fa";
import { useNavigate } from "react-router-dom";
import Button from "../../../components/Button";
import Card from "../../../components/Card";
import { Navbar } from "../../../components/Navbar";
import Spacing from "../../../components/Spacing";

const confiseries = [
  { name: "Pop corn", price: 5, icon: "🍿" },
  { name: "Bonbons", price: 4, icon: "🍬" },
  { name: "Boisson", price: 3, icon: "🍹" },
];

const ConfiseriesSelectionPage = () => {
  const [selectedConfiseries, setSelectedConfiseries] = useState<{
    [key: string]: number;
  }>({});
  const navigate = useNavigate();

  const handleQuantityChange = (type: string, amount: number) => {
    setSelectedConfiseries((prev) => {
      const newQuantity = (prev[type] || 0) + amount;
      if (newQuantity < 0) return prev;
      return { ...prev, [type]: newQuantity };
    });
  };

  const total = Object.entries(selectedConfiseries).reduce(
    (acc, [key, quantity]) =>
      acc + (confiseries.find((c) => c.name === key)?.price || 0) * quantity,
    0
  );

  return (
    <div className="text-white min-h-screen">
      <Navbar />
      <Spacing size="lg" />
      <div className="max-w-screen-lg mx-auto px-6 py-10 text-center">
        <h1 className="text-3xl font-bold mb-8">
          Sélectionner vos confiseries
        </h1>

        <Card className="w-full max-w-3xl mx-auto p-6 sm:p-8">
          {confiseries.map(({ name, price, icon }) => (
            <div
              key={name}
              className="flex items-center justify-between border-b border-gray-500 py-4 px-4"
            >
              <div className="flex items-center space-x-3">
                <span className="text-2xl">{icon}</span>
                <span className="font-bold">{name}</span>
              </div>
              <div className="flex items-center space-x-3">
                <button
                  className="p-2 bg-gray-700 rounded-full disabled:opacity-50"
                  onClick={() => handleQuantityChange(name, -1)}
                  disabled={!selectedConfiseries[name]}
                >
                  <FaMinus />
                </button>
                <span className="w-6 text-center">
                  {selectedConfiseries[name] || 0}
                </span>
                <button
                  className="p-2 bg-primary rounded-full"
                  onClick={() => handleQuantityChange(name, 1)}
                >
                  <FaPlus />
                </button>
              </div>
            </div>
          ))}

          <div className="flex flex-col sm:flex-row justify-end items-center mt-6 px-4 sm:space-x-4 space-y-2 sm:space-y-0">
            <button
              className="text-gray-400 hover:text-white"
              onClick={() => navigate("/paiement")}
            >
              Passer
            </button>
            <Button
              className={`w-full sm:w-auto px-6 py-2 transition duration-300 ${
                total === 0
                  ? "bg-gray-500 cursor-not-allowed"
                  : "bg-primary hover:bg-hover"
              }`}
              disabled={total === 0}
              onClick={() => navigate("/paiement")}
            >
              Continuer
            </Button>
          </div>
        </Card>
      </div>
    </div>
  );
};

export default ConfiseriesSelectionPage;
