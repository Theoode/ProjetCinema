using Microsoft.EntityFrameworkCore;
using ScrynDataProvider.Repositories;
using ScrynDomain.DataAdapters;
using ScrynDomain.Entities;
using WebApplication1.Data;

namespace WebApplication1.Repositories;

public class ReservationRepository(ScrynDbContext context) : Repository<Reservation>(context), IReservationRepository
{
    public Task<Reservation?> FindReservationComplet(long id_reservation)
    {
        ArgumentNullException.ThrowIfNull(context.Reservations);
        return context.Reservations
            .Include(r => r.ContientDans)
            .Include(r => r.Seance)
            .FirstOrDefaultAsync(r => r.id_reservation == id_reservation);
    }
}