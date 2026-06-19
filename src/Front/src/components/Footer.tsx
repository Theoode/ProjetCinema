import { FaInstagram, FaYoutube } from "react-icons/fa6";

import Spacing from "./Spacing";

export const Footer = () => {
  const InstagramIcon = () => <FaInstagram size={24} />;
  const YoutubeIcon = () => <FaYoutube size={24} />;

  return (
    <footer className="bg-gradient-to-t from-primary to-background text-black p-6 mt-8">
      <Spacing size="lg" />
      <Spacing size="md" />
      <div className="grid grid-cols-1 md:grid-cols-4 gap-y-6 text-center md:text-left">
        <div className="flex flex-col items-center md:items-start space-y-4">
          <div className="flex space-x-4">
            <InstagramIcon />
            <YoutubeIcon />
          </div>
        </div>

        <div>
          <h3 className="font-bold mb-5">Liens utiles</h3>
          <ul className="mt-2 space-y-2">
            <li>Conditions générales de vente</li>
            <li>Charte de données personnelles</li>
            <li>Conditions générales d’utilisation</li>
            <li>Mentions légales</li>
          </ul>
        </div>

        <div>
          <h3 className="font-bold mb-5">Service client</h3>
          <ul className="mt-2 space-y-2">
            <li>Centre d'aide</li>
            <li>Nous contacter</li>
            <li>Mon compte</li>
          </ul>
        </div>

        <div>
          <h3 className="font-bold mb-5">Scryn</h3>
          <ul className="mt-2 space-y-2">
            <li>À propos</li>
            <li>Films</li>
            <li>Offres</li>
            <li>Genres</li>
          </ul>
        </div>
      </div>

      <Spacing />
    </footer>
  );
};
