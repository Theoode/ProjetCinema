import { createBrowserRouter, RouterProvider } from "react-router-dom";
import RequireAuth from "./components/RequireAuth";
import AdminLayout from "./layouts/Admin";
import UserLayout from "./layouts/User";
import { AdminFilms } from "./pages/admin-films";
import { Clients } from "./pages/clients";
import ConfiseriesSelectionPage from "./pages/confiseries-selection/ui/ConfiseriesSelectionPage";
import { Dashboard } from "./pages/dashboard";
import { EditFilm } from "./pages/edit-film";
import { GestionFilms } from "./pages/gestion-films";
import { Home } from "./pages/home";
import { InfoFilm } from "./pages/info-film";
import LoginPage from "./pages/login/ui/LoginPage";
import MovieDetailsPage from "./pages/movie-details/ui/MovieDetailsPage";
import NewFilm from "./pages/new-film/ui/NewFilm";
import NewSeance from "./pages/new-seance/ui/NewSeance";
import NowPlayingPage from "./pages/now-playing/ui/NowPlayingPage";
import { Parcours } from "./pages/parcours";
import PaymentPage from "./pages/payment/ui/PaymentPage";
import ProfilePage from "./pages/profil/ui/ProfilePage";
import RegisterPage from "./pages/register/ui/RegisterPage";
import ReservationDetailsPage from "./pages/reservation-details/ui/ReservationDetailsPage";
import { Seance } from "./pages/seance";
import SeatSelectionPage from "./pages/seat-selection/ui/SeatSelectionPage";
import SuccessPage from "./pages/success-page/ui/SuccessPage";
import TarifSelectionPage from "./pages/tarif-selection/ui/TarifSelectionPage";
import UpComingMoviesPage from "./pages/up-coming-movies/ui/UpComingMoviesPage";
import {InfoSeance} from "./pages/info-seance";

const router = createBrowserRouter([
  {
    path: "/",
    element: <UserLayout />,
    children: [
      {
        index: true,
        element: <Home />,
      },
      {
        path: "parcours",
        element: <Parcours />,
      },
      {
        path: "films-a-l-affiche",
        element: <NowPlayingPage />,
      },
      {
        path: "prochaines-sorties",
        element: <UpComingMoviesPage />,
      },
      {
        path: "films/:id",
        element: <MovieDetailsPage />,
      },
      {
        path: "films/:id/seance/:seanceId",
        element: (
          <RequireAuth>
            <SeatSelectionPage />
          </RequireAuth>
        ),
      },
      {
        path: "login",
        element: <LoginPage />,
      },
      {
        path: "register",
        element: <RegisterPage />,
      },
      {
        path: "tarif",
        element: <TarifSelectionPage />,
      },
      {
        path: "confiseries",
        element: <ConfiseriesSelectionPage />,
      },
      {
        path: "paiement",
        element: <PaymentPage />,
      },
      {
        path: "success",
        element: <SuccessPage />,
      },
      {
        path: "profil",
        element: <ProfilePage />,
      },
      {
        path: "reservation/:id",
        element: <ReservationDetailsPage />,
      },
    ],
  },
  {
    path: "/admin",
    element: <RequireAuth><AdminLayout /></RequireAuth>,
    children: [
      {
        index: true,
        element: <Dashboard />,
      },
      {
        path: "admin-films",
        element: <AdminFilms />,
      },
      {
        path: "film/:id",
        element: <InfoFilm />,
      },
      {
        path: "clients",
        element: <Clients />,
      },
      {
        path: "gestion-films",
        element: <GestionFilms />,
      },
      {
        path: "new-film",
        element: <NewFilm />,
      },
      {
        path: "edit-film/:id",
        element: <EditFilm />,
      },
      {
        path: "seance",
        element: <Seance />,
      },
      {
        path: "new-seance",
        element: <NewSeance />,
      },
      {
        path: "info-seance/:id",
        element: <InfoSeance />,
      },
    ],
  },
]);

export const AppRouter = () => {
  return <RouterProvider router={router} />;
};
