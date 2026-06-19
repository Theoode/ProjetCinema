using System.ComponentModel.Design;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.Exceptions.SalleExceptions;

namespace ScrynDomain.UseCases.FilmUseCases.Delete;

public class SupprimerReservation(IRepositoryFactory repositoryFactory)
{

    public async Task ExecuteAsync(Reservation reservation)
    {
        await CheckBusinessRules(reservation);
        await repositoryFactory.ReservationRepository().DeleteAsync(reservation);
        await repositoryFactory.SaveChangesAsync();
    }

    private async Task CheckBusinessRules(Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);
        ArgumentNullException.ThrowIfNull(repositoryFactory.ReservationRepository());

        var reservations = await repositoryFactory.ReservationRepository()
            .FindByConditionAsync(r => r.id_reservation.Equals(reservation.id_reservation));

        if (!reservations.Any()) throw new SalleDoesntExistException();
    }

    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur) || role.Equals(Roles.Employe);
    }
}