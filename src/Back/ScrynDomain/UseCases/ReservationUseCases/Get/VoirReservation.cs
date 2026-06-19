using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.Exceptions;
using ScrynDomain.Exceptions.ReservationExceptions;
using ScrynDomain.UseCases.UserUseCases;

namespace ScrynDomain.UseCases.ReservationUseCases.Get;

public class VoirReservation(IRepositoryFactory repositoryFactory)
{
    public async Task<Reservation?> ExecuteAsync(long idReservation, string mail)
    {
        await CheckBusinessRules(idReservation, mail);
        Reservation? reservation = await repositoryFactory.ReservationRepository().FindReservationComplet(idReservation);
        return reservation;
    }

    private async Task CheckBusinessRules(long idReservation, string mail)
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ArgumentNullException.ThrowIfNull(repositoryFactory.ReservationRepository());
        ArgumentNullException.ThrowIfNull(idReservation);
        var reservation = await repositoryFactory.ReservationRepository().FindAsync(idReservation);
        var user = await repositoryFactory.ScrynUser().FindByEmailAsync(mail);
        if(!reservation.Utilisateur.Equals(user))
            throw new UserExceptionReservation("Cet utilisateur n'a pas de réservations sur cette seance");
        
        
        if (reservation == null)
        {
            throw new ReservationDoesntExistException("Aucune réservation trouvée avec cet identifiant.");
        }
    }

    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Cineaste) || role.Equals(Roles.Directeur) || role.Equals(Roles.Employe);
    }
}