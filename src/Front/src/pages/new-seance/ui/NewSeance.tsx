import React, { useState } from "react";
import { useFilms } from "../../../hooks/useFilms";
import { useSeanceCreate } from "../../../hooks/useSeanceCreate";
import {Link} from "react-router-dom";

export default function NewSeance() {
    const { data: films, isLoading, error } = useFilms();
    const { mutate: createSeance, isPending, isSuccess, isError } = useSeanceCreate();

    const [selectedFilmId, setSelectedFilmId] = useState<number | null>(null);
    const [selectedSalleId, setSelectedSalleId] = useState<number>(1); // par défaut salle 1
    const [date, setDate] = useState("");
    const [heure, setHeure] = useState("");

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        if (!selectedFilmId || !date || !heure || !selectedSalleId) {
            return alert("Tous les champs sont requis !");
        }

        const dateTime = new Date(`${date}T${heure}`).toISOString();

        createSeance({
            date_seance: dateTime,
            fk_film: selectedFilmId,
            fk_salle: selectedSalleId,
        });
    };

    return (
        <div className="relative">
            <p className="text-lg text-gray-400 mb-2">Ajout</p>
            <h1 className="text-6xl mb-10">Nouvelle séance</h1>
            <Link
                to="/admin/seance"
                className="absolute top-0 right-8 bg-[#EEAE4A] text-white px-6 py-3 rounded-lg shadow-[0_0_20px_3px_rgba(238,174,74,1)] hover:bg-[#EEAF5A] transition scale-100 hover:scale-105 duration-1000"
            >
                Retour
            </Link>

            <div className="p-10 text-white">
                <form className="flex flex-wrap gap-8" onSubmit={handleSubmit}>
                    {/* Film */}
                    <div className="flex flex-col w-[40%]">
                        <label className="mb-2">Film</label>
                        <div className="bg-[#161616] p-6 rounded-full shadow-[0_0_20px_3px_rgba(238,174,74,1)]">
                            <select
                                value={selectedFilmId ?? ""}
                                onChange={(e) => setSelectedFilmId(Number(e.target.value))}
                                className="w-full bg-transparent rounded-full p-2 focus:outline-none text-white"
                            >
                                <option value="">Sélectionner un film</option>
                                {isLoading && <option disabled>Chargement...</option>}
                                {error && <option disabled>Erreur</option>}
                                {films?.map((film) => (
                                    <option key={film.id_film} value={film.id_film}>
                                        {film.nom_film}
                                    </option>
                                ))}
                            </select>
                        </div>
                    </div>

                    <div className="flex flex-col w-[40%]">
                        <label className="mb-2">Salle</label>
                        <div className="bg-[#161616] p-6 rounded-full shadow-[0_0_20px_3px_rgba(238,174,74,1)]">
                            <select
                                value={selectedSalleId}
                                onChange={(e) => setSelectedSalleId(Number(e.target.value))}
                                className="w-full bg-transparent rounded-full p-2 focus:outline-none text-white"
                            >
                                <option value={1}>Salle 1</option>
                                <option value={2}>Salle 2</option>
                                <option value={3}>Salle 3</option>
                            </select>
                        </div>
                    </div>

                    <div className="flex flex-col w-[40%]">
                        <label className="mb-2">Date</label>
                        <div className="bg-[#161616] p-6 rounded-full shadow-[0_0_20px_3px_rgba(238,174,74,1)]">
                            <input
                                type="date"
                                value={date}
                                onChange={(e) => setDate(e.target.value)}
                                className="w-full bg-transparent rounded-full p-2 focus:outline-none"
                            />
                        </div>
                    </div>

                    <div className="flex flex-col w-[40%]">
                        <label className="mb-2">Heure</label>
                        <div className="bg-[#161616] p-6 rounded-full shadow-[0_0_20px_3px_rgba(238,174,74,1)]">
                            <input
                                type="time"
                                value={heure}
                                onChange={(e) => setHeure(e.target.value)}
                                className="w-full bg-transparent rounded-full p-2 focus:outline-none"
                            />
                        </div>
                    </div>

                    <div className="w-full text-center pt-10">
                        <button
                            type="submit"
                            disabled={isPending}
                            className="bg-[rgba(238,174,74,1)] text-black px-8 py-3 rounded-full text-lg font-semibold hover:brightness-110 transition-all"
                        >
                            {isPending ? "Ajout..." : "Créer la séance"}
                        </button>

                        {isSuccess && <p className="text-green-500 mt-4">Séance ajoutée avec succès !</p>}
                        {isError && (
                            <p className="text-red-500 mt-4">
                                Erreur lors de l'ajout de la séance : {(error as Error).message}
                            </p>
                        )}
                    </div>
                </form>
            </div>
        </div>
    );
}