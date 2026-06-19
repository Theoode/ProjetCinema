using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.Exceptions.ReservationExceptions;

namespace ScrynDomain.UseCases.PaiementUseCases.Create;

public class AjouterPaiement (IRepositoryFactory repositoryFactory)
{
 
    public async Task<Paiement> ExecuteAsync(float montant, string methode, DateTime date_paiement, long fk_reservation)
    {
        var reservation = repositoryFactory.ReservationRepository().FindAsync(fk_reservation);
        if (reservation == null)
            throw new ReservationDoesntExistException("la réservation n'existe pas");
        
        Paiement paiement = new Paiement
        {
            montant = montant,
            methode = methode,
            date_paiement = date_paiement,
            Reservation = reservation.Result
        };
        return await ExecuteAsync(paiement);
    }

    public async Task<Paiement> ExecuteAsync(Paiement paiement)
    {
        CheckBusinessRules(paiement);
        Paiement paiementRe = await repositoryFactory.PaiementRepository().CreateAsync(paiement);
        repositoryFactory.PaiementRepository().SaveChangesAsync().Wait();
        return paiementRe;
    }

    private async Task CheckBusinessRules(Paiement paiement)
    {
        ArgumentNullException.ThrowIfNull(paiement);
        ArgumentNullException.ThrowIfNull(repositoryFactory.PaiementRepository());
        
    }
    
}