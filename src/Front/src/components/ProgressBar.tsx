import React from "react";

interface ProgressBarProps {
    progress: number;
    value: number;
}

export const ProgressBar: React.FC<ProgressBarProps> = ({ progress, value }) => {    return (
        <div className="relative w-[90%] max-w-auto h-6 rounded-full bg-white shadow-md">
            <div
                className="absolute top-0 left-0 h-full bg-primary rounded-full shadow-[0_0_7px_3px_rgba(238,174,74,0.5)]"
                style={{width: `${progress}%`}}
            ></div>
            <span className="absolute right-0 -mr-8 text-xs">{value}</span>
        </div>
    );
};