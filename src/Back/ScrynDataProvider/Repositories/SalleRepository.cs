using Microsoft.EntityFrameworkCore;
using ScrynDataProvider.Repositories;
using ScrynDomain.DataAdapters;
using ScrynDomain.Entities;
using WebApplication1.Data;

namespace WebApplication1.Repositories;

public class SalleRepository(ScrynDbContext context) : Repository<Salle>(context), ISalleRepository
{
    public Task<Salle?> FindSalleComplet(long id_salle)
    {
        ArgumentNullException.ThrowIfNull(Context.Salles);
        return context.Salles
            .Include(s => s.ContenuDans)
            .Include(s => s.PresenteDans)
            .FirstOrDefaultAsync(salle => salle.id_salle == id_salle);
    }
}