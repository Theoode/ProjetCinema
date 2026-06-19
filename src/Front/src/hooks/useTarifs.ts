import { useQuery } from "@tanstack/react-query";

export type Tarif = {
  id_tarif: number;
  nom_tarif: string;
  valeur: number;
  date_deb: string;
  date_fin: string;
};

const API_URL = "http://35.181.160.232:5000/api/Tarif";

const fetchTarifs = async (): Promise<Tarif[]> => {
  const response = await fetch(API_URL);

  if (!response.ok) {
    throw new Error("Erreur lors du chargement des tarifs");
  }

  return response.json();
};

export const useTarifs = () => {
  return useQuery({
    queryKey: ["tarifs"],
    queryFn: fetchTarifs,
  });
};
