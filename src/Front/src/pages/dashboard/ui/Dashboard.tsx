import { ProgressBar } from "../../../components/ProgressBar";
import Gladiator2 from "../../../assets/gladiator2.png";
import Enfants from "../../../assets/enfants.png";
import Montecristo from "../../../assets/montecristo.png";
import userIcon from "../../../assets/user-icon.png";

const data = [
    { month: "Avril", value: 850 },
    { month: "Mars", value: 500 },
    { month: "Février", value: 700 },
];

const posters = [
    { poster: Gladiator2, entries: "2.3k" },
    { poster: Montecristo, entries: "2.0k" },
    { poster: Enfants, entries: "1.8k" },
];

const revenues = [
    { value: "2.3k€", month: "Février" },
    { value: "1.8k€", month: "Janvier" },
    { value: "2.0k€", month: "Décembre" }
];

export const Dashboard: React.FC = () => {
    const maxEntries = Math.max(...data.map(entry => entry.value));

    return (
        <div>
            <p>Résumé du mois d'</p>
            <h1 className={"text-6xl"}>Avril</h1>

            <div className="mt-10 space-y-6">
                {data.map(({month, value}) => {
                    const percentage = (value / maxEntries) * 100;

                    return (
                        <div key={month}>
                            <p>{month}</p>
                            <ProgressBar progress={percentage} value={value}/>
                        </div>
                    );
                })}
            </div>

            <div className="mt-32 space-y-6">
                <p>Plus grand succès ce mois-ci :</p>
            </div>
            <div className="grid grid-cols-3 gap-x-14 mr-[10vw] mt-10">
                {posters.map(({poster, entries}, index) => (
                    <div key={index} className="text-center">
                        <img
                            src={poster}
                            alt={`Affiche ${index + 1}`}
                            className="rounded-2xl w-full h-full object-cover"
                        />
                        <p className="text-sm mt-1">{entries} entrées</p>
                    </div>
                ))}
            </div>
            <div className="mt-32 space-y-6">
                <p>Chiffre d'affaire</p>
            </div>
            <div className="flex justify-center gap-10 mt-6">
                {revenues.map(({value, month}, index) => (
                    <div key={index} className="text-center mt-10 ">
                        <h1 className="text-6xl"
                            style={{
                                color: "rgb(238, 174, 74)",
                                textShadow: "0 0 17px rgba(238, 174, 74, 0.8)"
                            }}
                        >
                            {value}
                        </h1>
                        <p className="text-sm mt-1">{month}</p>
                    </div>
                ))}
            </div>
            <div className="flex items-center justify-center mt-32 w-full mb-[50px]">
                <img src={userIcon} alt="User Icon" className="w-[20%] h-[20%] object-contain"/>
                <h1 className="ml-6 text-4xl"
                    style={{
                        textShadow: "0 0 17px rgba(238, 174, 74, 0.8)"
                    }}>
                    574 Nouveaux utilisateurs ce mois-ci
                </h1>
            </div>
        </div>
    );
};
