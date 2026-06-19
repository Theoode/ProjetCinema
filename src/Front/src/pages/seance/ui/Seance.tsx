import { Link } from "react-router-dom";
import { SeanceButton } from "../../../components/admin/SeanceButton";
import {useSeances} from "../../../hooks/useAllSeance";

export const Seance: React.FC = () => {
    const { data: seances, isLoading, isError } = useSeances();

    const groupedBySalle = seances?.reduce((acc, seance) => {
        if (!acc[seance.fk_salle]) acc[seance.fk_salle] = [];
        acc[seance.fk_salle].push(seance);
        return acc;
    }, {} as Record<number, typeof seances>) ?? {};

    return (
        <div>
            <div className="relative">
                <p>Gestion</p>
                <h1 className="text-6xl">Séances</h1>

                <Link
                    to="/admin/new-seance"
                    className="absolute top-0 right-8 bg-[#EEAE4A] text-white px-6 py-3 rounded-lg shadow-[0_0_20px_3px_rgba(238,174,74,1)] hover:bg-[#EEAF5A] transition scale-100 hover:scale-105 duration-1000"
                >
                    + Ajouter une séance
                </Link>

                <div className="mt-20 space-y-10">
                    {isLoading && <p>Chargement des séances...</p>}
                    {isError && <p>Erreur lors du chargement des séances.</p>}

                    {Object.entries(groupedBySalle).map(([salleId, seances]) => {
                        const sorted = seances.sort(
                            (a, b) =>
                                new Date(b.date_seance).getTime() -
                                new Date(a.date_seance).getTime()
                        );

                        return (
                            <div key={salleId}>
                                <p className="text-2xl mb-3">Salle {salleId}</p>
                                <div className="flex flex-wrap gap-4">
                                    {sorted.map((seance) => (
                                        <Link
                                            key={seance.id_seance}
                                            to={`/admin/info-seance/${seance.id_seance}`}
                                        >
                                            <SeanceButton
                                                jour={new Date(seance.date_seance).toLocaleDateString("fr-FR", {
                                                    weekday: "short",
                                                    day: "numeric",
                                                    month: "short",
                                                })}
                                                horaire={new Date(seance.date_seance).toISOString().slice(11, 16)}
                                            />
                                        </Link>
                                    ))}
                                </div>
                            </div>
                        );
                    })}
                </div>
            </div>
        </div>
    );
};