import { useMutation } from "@tanstack/react-query";

type PaiementData = {
  montant: number;
  methode: string;
  date_paiement: string;
};

const simulatePaiement = async ({
  reservationId,
  paiement,
}: {
  reservationId: number;
  paiement: PaiementData;
}) => {
  await new Promise((resolve) => setTimeout(resolve, 1000));
  return { success: true, paiement };
};

export const usePaiement = () => {
  return useMutation({
    mutationFn: simulatePaiement,
  });
};
