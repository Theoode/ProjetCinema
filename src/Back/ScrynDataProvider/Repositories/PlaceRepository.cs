using Microsoft.EntityFrameworkCore;
using ScrynDataProvider.Repositories;
using ScrynDomain.DataAdapters;
using ScrynDomain.Entities;
using WebApplication1.Data;

namespace Scryn.Repositories;

public class PlaceRepository(ScrynDbContext context) : Repository<Place>(context), IPlaceRepository
{
    public Task<Place?> FindPlaceComplet(long idPlace)
    {
        return context.Places
            .Include(r => r.Reservation)
            .Include(s => s.FaitPartie)
            .FirstOrDefaultAsync(p => p.id_place == idPlace);
    }
}