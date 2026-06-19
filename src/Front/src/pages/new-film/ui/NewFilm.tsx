import React, { useState } from "react";
import { useCreateFilm } from "../../../hooks/useCreateFilm";

export default function NewFilm() {
    const [form, setForm] = useState({
        title: "",
        genre: "Drame",
        duration: "",
        releaseDate: "",
        description: "",
        poster: "",
    });

    const { createFilm, loading, error, success } = useCreateFilm();

    const handleChange = (
        e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>
    ) => {
        setForm({ ...form, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        if (!form.poster) return alert("Ajoute une URL d’image !");

        await createFilm({
            nom_film: form.title,
            auteur: "Auteur par défaut",
            duree: form.duration.toString(),
            date_sortie: new Date(form.releaseDate).toISOString(),
            description: form.description,
            affiche: form.poster,
        });
    };

    return (
        <>
            <p className="text-lg text-gray-400 mb-2">Ajout</p>
            <h1 className="text-6xl mb-10">Nouveau film</h1>

            <div className="p-10 text-white">
                <form className="flex flex-wrap gap-8" onSubmit={handleSubmit}>
                    <div className="flex flex-col w-[40%]">
                        <p className="mb-2">Titre</p>
                        <div className="bg-[#161616] p-6 rounded-full shadow-[0_0_20px_3px_rgba(238,174,74,1)]">
                            <input
                                type="text"
                                name="title"
                                value={form.title}
                                onChange={handleChange}
                                className="w-full bg-transparent rounded-full p-2 focus:outline-none"
                            />
                        </div>
                    </div>

                    <div className="flex flex-col w-[40%]">
                        <p className="mb-2">Genre</p>
                        <div className="bg-[#161616] p-6 rounded-full shadow-[0_0_20px_3px_rgba(238,174,74,1)]">
                            <select
                                name="genre"
                                value={form.genre}
                                onChange={handleChange}
                                className="w-full bg-transparent rounded-full p-2 focus:outline-none"
                            >
                                <option>Drame</option>
                                <option>Action</option>
                                <option>Comédie</option>
                                <option>Science-Fiction</option>
                                <option>Documentaire</option>
                            </select>
                        </div>
                    </div>

                    <div className="flex flex-col w-[40%]">
                        <p className="mb-2">Durée (en minutes)</p>
                        <div className="bg-[#161616] p-6 rounded-full shadow-[0_0_20px_3px_rgba(238,174,74,1)]">
                            <input
                                type="number"
                                name="duration"
                                value={form.duration}
                                onChange={handleChange}
                                className="w-full bg-transparent rounded-full p-2 focus:outline-none"
                            />
                        </div>
                    </div>

                    <div className="flex flex-col w-[40%]">
                        <p className="mb-2">Date de sortie</p>
                        <div className="bg-[#161616] p-6 rounded-full shadow-[0_0_20px_3px_rgba(238,174,74,1)]">
                            <input
                                type="date"
                                name="releaseDate"
                                value={form.releaseDate}
                                onChange={handleChange}
                                className="w-full bg-transparent rounded-full p-2 focus:outline-none"
                            />
                        </div>
                    </div>

                    <div className="flex flex-col w-[20vw] h-full">
                        <p className="mb-2">Affiche (URL)</p>
                        <div className="bg-[#161616] p-6 rounded-[2rem] shadow-[0_0_20px_3px_rgba(238,174,74,1)]">
                            <input
                                type="text"
                                name="poster"
                                placeholder="https://..."
                                value={form.poster}
                                onChange={handleChange}
                                className="w-full bg-transparent p-2 rounded-xl focus:outline-none"
                            />
                            {form.poster && (
                                <img
                                    src={form.poster}
                                    alt="Aperçu"
                                    className="w-full h-auto mt-4 rounded-xl"
                                />
                            )}
                        </div>
                    </div>

                    <div className="flex flex-col w-[30vw] h-full">
                        <p className="mb-2">Description</p>
                        <div className="bg-[#161616] p-6 rounded-[2rem] shadow-[0_0_20px_3px_rgba(238,174,74,1)]">
                            <textarea
                                name="description"
                                rows={5}
                                value={form.description}
                                onChange={handleChange}
                                className="w-[30vw] h-full bg-transparent rounded-xl p-2 focus:outline-none resize-none"
                            />
                        </div>
                    </div>

                    <div className="w-full text-center pt-10">
                        <button
                            type="submit"
                            disabled={loading}
                            className="bg-[rgba(238,174,74,1)] text-black px-8 py-3 rounded-full text-lg font-semibold hover:brightness-110 transition-all"
                        >
                            {loading ? "Envoi..." : "Créer le film"}
                        </button>
                        {success && <p className="text-green-500 mt-4">Film ajouté avec succès !</p>}
                        {error && <p className="text-red-500 mt-4">{error}</p>}
                    </div>
                </form>
            </div>
        </>
    );
}