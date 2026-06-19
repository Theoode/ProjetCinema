import React, { ReactNode } from "react";

interface CardProps {
  children: ReactNode;
  className?: string;
}

const Card: React.FC<CardProps> = ({ children, className }) => {
  return (
    <div
      className={`p-10 bg-background rounded-lg max-w-md w-full border border-gray-700 shadow-[0_0_20px_3px_theme('colors.primary')] ${className}`}
    >
      {children}
    </div>
  );
};

export default Card;
