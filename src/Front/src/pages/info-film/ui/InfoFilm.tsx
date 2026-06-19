import React from 'react';
import { useParams } from 'react-router-dom';
import { useFilmID } from '../../../hooks/useFilmID';

export const InfoFilm: React.FC = () => {
    const { id } = useParams<{ id?: string }>();

    if (!id) {
        return <div>Identifiant du film manquant.</div>;
    }

    const filmId = parseInt(id, 10);
    const { film, loading, error } = useFilmID(filmId);

    if (loading) {
        return <p>Chargement en cours...</p>;
    }

    if (error) {
        return <p>Erreur : {error}</p>;
    }

    if (!film) {
        return <p>Film non trouvé.</p>;
    }

    return (
        <div>
            <p>Informations</p>
            <h1 className="text-6xl">Film</h1>
            <div className="flex items-start gap-10 p-10">
                <div className="w-1/3">
                    <img
                        src={`${film.affiche}`}
                        alt={`Affiche de ${film.nom_film}`}
                        className="rounded-2xl w-full h-auto object-cover"
                    />
                </div>
                <div className="w-2/3">
                    <h1 className="text-4xl font-bold mb-4">{film.nom_film}</h1>
                    <p><strong>Durée :</strong> {film.duree}</p>
                    <p><strong>Date de sortie :</strong> {film.date_sortie}</p>
                    <div className="mt-10">
                        <p>{film.description}</p>
                    </div>
                </div>
            </div>
        </div>
    );
};
