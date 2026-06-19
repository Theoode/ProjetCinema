using Microsoft.EntityFrameworkCore;
using ScrynDataProvider.Repositories;
using ScrynDomain.DataAdapters;
using ScrynDomain.Entities;
using WebApplication1.Data;

namespace Scryn.Repositories;

public class SeanceRepository(ScrynDbContext context) : Repository<Seance>(context), ISeanceRepository
{
    public Task<Seance?> FindSeanceComplet(long idSeance)
    {
        return context.Seance.Include(s => s.AppliqueSur)
            .Include(s => s.Film)
            .Include(s => s.Salle)
            .Include(s => s.ContenuDans)
            .FirstOrDefaultAsync(s => s.id_seance == idSeance);
    }

    public async Task AddAsync(Seance seance)
    {
        await context.Seance.AddAsync(seance);
        await context.SaveChangesAsync();
    }
}