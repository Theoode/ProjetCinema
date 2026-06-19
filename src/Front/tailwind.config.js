/** @type {import('tailwindcss').Config} */
export default {
  content: ["./index.html", "./src/**/*.{js,ts,jsx,tsx}"],
  theme: {
    extend: {
      colors: {
        primary: "#EEAE4A", // jaune principal
        hover: "#F1B75F",
        background: "#161717", // Noir foncé pour le fond
        accent: "#ffcc00", // Jaune pour les accents
      },
    },
  },
  plugins: [],
};
