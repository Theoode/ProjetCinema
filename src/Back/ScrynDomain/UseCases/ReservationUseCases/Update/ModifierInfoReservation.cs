using System.Runtime.InteropServices.JavaScript;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;

public class ModifierInfoReservation(IRepositoryFactory repositoryFactory) {


    public async Task ExecuteAsync(List<Place>? places,Seance seance, DateTime date_reservation, Reservation reservation){
        
        var reservationUpdate = await repositoryFactory.ReservationRepository().FindAsync(reservation.id_reservation);
        await CheckBusinessRules(reservationUpdate);
        reservationUpdate.date_reservation = date_reservation;
        reservationUpdate.ContientDans = places;
        reservationUpdate.Seance = seance;
        

        
       await repositoryFactory.ReservationRepository().UpdateAsync(reservationUpdate);
       await repositoryFactory.ReservationRepository().SaveChangesAsync();
    }

    private async Task CheckBusinessRules(Reservation reservation){
        ArgumentNullException.ThrowIfNull(reservation, nameof(reservation));
        ArgumentNullException.ThrowIfNull(repositoryFactory.ReservationRepository());
        //suite à réfléchir
    }

}