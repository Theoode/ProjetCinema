import { useEffect, useState } from "react";
import { FiMenu, FiX } from "react-icons/fi";
import { useNavigate } from "react-router-dom";
import logo from "../assets/logo.png";
import Button from "./Button";

export const Navbar = () => {
  const [isOpen, setIsOpen] = useState(false);
  const [isDropdownOpen, setIsDropdownOpen] = useState(false);
  const [user, setUser] = useState<{ email: string } | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    const storedUser = localStorage.getItem("user");
    if (storedUser) {
      setUser(JSON.parse(storedUser));
    }
  }, []);

  const handleLogout = () => {
    localStorage.removeItem("user");
    setUser(null);
    navigate("/");
  };

  return (
    <>
      <nav className="fixed top-0 left-0 w-full z-50 flex items-center justify-between p-4 text-white bg-gradient-to-b from-background to-transparent">
        <div className="flex items-center space-x-6 relative">
          <div className="absolute -z-10 w-24 h-24 bg-primary opacity-40 rounded-full blur-3xl"></div>
          <a href="/" className="relative z-10">
            <img
              src={logo}
              alt="Scryn Logo"
              className="h-12 transition-transform duration-300 transform hover:scale-105 cursor-pointer"
            />
          </a>
          <div className="hidden lg:flex space-x-6">
            <a href="/" className="hover:text-primary transition duration-300">
              Accueil
            </a>
            <a
              href="/films-a-l-affiche"
              className="hover:text-primary transition duration-300"
            >
              Films à l'affiche
            </a>
            <a
              href="/prochaines-sorties"
              className="hover:text-primary transition duration-300"
            >
              Prochaines sorties
            </a>
          </div>
        </div>

        <div className="hidden lg:flex space-x-4 relative">
          <Button
            className="bg-primary hover:bg-hover transition duration-300"
            onClick={() => navigate("/films-a-l-affiche")}
          >
            Réserver un billet
          </Button>

          {user ? (
            <div className="relative">
              <Button
                className="text-white border-2 border-primary px-6 py-3 hover:bg-primary transition duration-300"
                onClick={() => setIsDropdownOpen(!isDropdownOpen)}
              >
                {user.email}
              </Button>

              {isDropdownOpen && (
                <div className="absolute right-0 mt-2 w-40 bg-background rounded-lg shadow-lg text-white">
                  <Button onClick={() => navigate("/profil")}>Profil</Button>
                  <Button
                    className="block w-full text-left px-4 py-2 hover:bg-red-700 transition"
                    onClick={handleLogout}
                  >
                    Déconnexion
                  </Button>
                </div>
              )}
            </div>
          ) : (
            <Button
              className="border-2 border-solid border-primary hover:bg-primary transition duration-300"
              onClick={() => navigate("/login")}
            >
              Se connecter
            </Button>
          )}
        </div>

        <button
          className="lg:hidden text-white text-2xl"
          onClick={() => setIsOpen(true)}
        >
          <FiMenu />
        </button>
      </nav>

      {isOpen && (
        <div className="fixed inset-0 bg-white text-black flex flex-col items-center justify-center z-50 lg:hidden">
          <img src={logo} alt="Scryn Logo" className="h-16 mb-20" />

          <nav className="flex flex-col space-y-6 text-xl items-center">
            <a href="/" className="hover:text-primary transition duration-300">
              Accueil
            </a>
            <a
              href="/films-a-l-affiche"
              className="hover:text-primary transition duration-300"
            >
              Films à l'affiche
            </a>
            <a
              href="/prochaines-sorties"
              className="hover:text-primary transition duration-300"
            >
              Prochaines sorties
            </a>
          </nav>

          <div className="mt-8 flex flex-col space-y-4">
            <Button
              className="bg-primary hover:bg-hover transition duration-300 px-6 py-3"
              onClick={() => {
                setIsOpen(false);
                navigate("/films-a-l-affiche");
              }}
            >
              Réserver un billet
            </Button>

            {user ? (
              <div className="relative flex items-center justify-center">
                <Button
                  className="text-black border-2 border-primary px-6 py-3 hover:bg-primary transition duration-300"
                  onClick={() => setIsDropdownOpen(!isDropdownOpen)}
                >
                  {user.email}
                </Button>

                {isDropdownOpen && (
                  <div className="absolute right-0 mt-2 w-40 bg-background rounded-lg shadow-lg text-white">
                    <Button onClick={() => navigate("/profil")}>Profil</Button>
                    <Button
                      className="block w-full text-left px-4 py-2 hover:bg-red-700 transition"
                      onClick={handleLogout}
                    >
                      Déconnexion
                    </Button>
                  </div>
                )}
              </div>
            ) : (
              <Button
                className="border-2 border-solid border-primary hover:bg-primary transition duration-300"
                onClick={() => navigate("/login")}
              >
                Se connecter
              </Button>
            )}
          </div>

          <button
            className="mt-20 flex flex-col items-center text-lg text-black"
            onClick={() => setIsOpen(false)}
          >
            <FiX className="text-4xl" />
            <span>Fermer</span>
          </button>
        </div>
      )}
    </>
  );
};
