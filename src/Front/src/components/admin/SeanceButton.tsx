import React from "react";

interface SeanceProps {
    jour: string;
    horaire: string;
}

export const SeanceButton: React.FC<SeanceProps> = ({ jour, horaire }) => {
    return (
        <div className="flex flex-col justify-center items-center border-2 border-[#EEAE4A] text-[#EEAE4A] rounded-2xl px-[60px] py-6 m-1 text-center hover:bg-[#EEAE4A] hover:text-white transition-colors duration-200">
            <div className="text-sm">{jour}</div>
            <div className="text-5xl">
                <h1>{horaire}</h1>
            </div>
        </div>
    );
};