using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.Exceptions.ReservationExceptions;

namespace ScrynDomain.UseCases.ReservationUseCases.Create;

public class AjouterUneReservation (IRepositoryFactory repositoryFactory)
{
    public async Task<Reservation> ExecuteAsync(DateTime dateReservation)
    {
        var reservation = new Reservation
        {
            date_reservation = dateReservation
        };
        return await ExecuteAsync(reservation);
    }
    public async Task<Reservation> ExecuteAsync(Reservation reservation)
    {
        CheckBusinessRules(reservation);
        Reservation reservationCree = await repositoryFactory.ReservationRepository().CreateAsync(reservation);
        repositoryFactory.ReservationRepository().SaveChangesAsync().Wait();
        return reservationCree;
    }

    private async Task CheckBusinessRules(Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);
        ArgumentNullException.ThrowIfNull(repositoryFactory.ReservationRepository());
        ArgumentNullException.ThrowIfNull(reservation.date_reservation);
        
        List<Reservation>? reservations = await repositoryFactory.ReservationRepository().FindByConditionAsync(r => r.id_reservation.Equals(reservation.id_reservation));
        if(reservations.Any()) throw
        new ReservationAlreadyExist("Cette réservation existe déja");
    }

    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur) || role.Equals(Roles.Employe);
    }
    //Ecrire fonction de rôle ici.
    
}