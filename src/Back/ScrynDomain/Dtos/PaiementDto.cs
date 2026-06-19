using ScrynDomain.Entities;

namespace ScrynDomain.Dtos;

public class PaiementDto
{
    public long id_paiement { get; set; }
    public float montant { get; set; }
    public string methode { get; set; }
    public DateTime date_paiement { get; set; }

    public long fk_reservation { get; set; }

    public PaiementDto ToDto(Paiement paiement)
    {
        id_paiement = paiement.id_paiement;
        montant = paiement.montant;
        methode = paiement.methode;
        date_paiement = paiement.date_paiement;
        fk_reservation = paiement.fk_reservation;
        return this;
    }

    public Paiement ToEntity()
    {
        return new Paiement
        {
            id_paiement = this.id_paiement,
            montant = this.montant,
            methode = this.methode,
            date_paiement = this.date_paiement,
            fk_reservation = this.fk_reservation,
            Reservation = new Reservation { id_reservation = fk_reservation }
        };
    }
}
