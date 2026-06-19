import { FC } from "react";
import { Link, useLocation } from "react-router-dom";

export const Sidebar: FC = () => {
  const location = useLocation();

  return (
      <div className="fixed left-[40px] top-1/2 transform -translate-y-1/2 w-[250px] h-auto bg-[#161616] text-white rounded-[22px] shadow-[0_0_20px_3px_rgba(238,174,74,1)] flex-shrink-0">
        <ul className="text-center">
          <li
              className={`px-6 py-8 text-xl rounded-[15px] cursor-pointer transition scale-100 hover:scale--105 duration-500 hover:shadow-[0_0_20px_3px_rgba(238,174,74,1)]
            ${location.pathname === "/admin" ? "bg-[rgba(238,174,74,1)] text-black" : ""}`}
          >
            <Link to="/admin" className="block w-full h-full">Accueil</Link>
          </li>
          <li
              className={`px-6 py-8 text-xl rounded-[15px] cursor-pointer transition scale-100 hover:scale--105 duration-500 hover:shadow-[0_0_20px_3px_rgba(238,174,74,1)]
            ${location.pathname === "/admin/gestion-films" ? "bg-[rgba(238,174,74,1)] text-black" : ""}`}
          >
            <Link to="/admin/gestion-films" className="block w-full h-full">Gestion Films</Link>
          </li>
          <li
              className={`px-6 py-8 text-xl rounded-[15px] cursor-pointer transition scale-100 hover:scale--105 duration-500 hover:shadow-[0_0_20px_3px_rgba(238,174,74,1)]
            ${location.pathname === "/admin/seance" ? "bg-[rgba(238,174,74,1)] text-black" : ""}`}
          >
            <Link to="/admin/seance" className="block w-full h-full">Gestion Séances</Link>
          </li>
          <li
              className={`px-6 py-8 text-xl rounded-[15px] cursor-pointer transition scale-100 hover:scale--105 duration-500 hover:shadow-[0_0_20px_3px_rgba(238,174,74,1)]
            ${location.pathname === "/admin/admin-films" ? "bg-[rgba(238,174,74,1)] text-black" : ""}`}
          >
            <Link to="/admin/admin-films" className="block w-full h-full">Films</Link>
          </li>
          <li
              className={`px-6 py-8 text-xl rounded-[15px] cursor-pointer transition scale-100 hover:scale--105 duration-500 hover:shadow-[0_0_20px_3px_rgba(238,174,74,1)]
            ${location.pathname === "/admin/clients" ? "bg-[rgba(238,174,74,1)] text-black" : ""}`}
          >
            <Link to="/admin/clients" className="block w-full h-full">Utilisateurs</Link>
          </li>
        </ul>
      </div>
  );
};