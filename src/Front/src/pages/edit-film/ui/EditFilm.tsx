import Montecristo from "../../../assets/montecristo.png";
import {useState} from "react";

export const EditFilm: React.FC = () => {
    const [form, setForm] = useState({
        title: "Montecristo",
        genre: "Drame",
        duration: "120",
        releaseDate: "2025-04-10",
        description: "Un film intense sur la vengeance.",
        poster: Montecristo,
    });

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
        setForm({...form, [e.target.name]: e.target.value});
    };

    return (
        <>
            <p className="text-lg text-gray-400 mb-2">Modification</p>
            <h1 className="text-6xl mb-10">Film</h1>

            <div className="p-10 text-white">
                <form className="flex flex-wrap gap-8">
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

                    {/* Date de sortie */}
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
                        <p className="mb-2">Affiche</p>
                        <div className="bg-[#161616] p-6 rounded-[2rem] shadow-[0_0_20px_3px_rgba(238,174,74,1)]">
                            <img
                                src={form.poster}
                                alt="Poster"
                                className="w-full h-full object-cover rounded-xl mb-4"
                            />
                            <input
                                type="file"
                                className="text-sm text-gray-400"
                                onChange={(e) => {
                                    if (e.target.files && e.target.files[0]) {
                                        const reader = new FileReader();
                                        reader.onload = () => {
                                            if (reader.result) {
                                                setForm({...form, poster: reader.result.toString()});
                                            }
                                        };
                                        reader.readAsDataURL(e.target.files[0]);
                                    }
                                }}
                            />
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
                            className="bg-[rgba(238,174,74,1)] text-black px-8 py-3 rounded-full text-lg font-semibold hover:brightness-110 transition-all"
                        >
                            Enregistrer
                        </button>
                    </div>
                </form>
            </div>
        </>
    );
};