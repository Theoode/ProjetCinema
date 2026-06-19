using Microsoft.EntityFrameworkCore;
using ScrynDataProvider.Repositories;
using ScrynDomain.DataAdapters;
using ScrynDomain.Entities;
using WebApplication1.Data;

namespace WebApplication1.Repositories;

public class FilmRepository (ScrynDbContext context) : Repository<Film>(context), IFilmRepository
{

    public Task<Film?> FindFilmComplet(long id_film)
    {
        ArgumentNullException.ThrowIfNull(context.Films);
        return context.Films
            .Include(f => f.FaitPartie)
            .Include(f => f.Seances)
                .ThenInclude(s => s.AppliqueSur)
            .Include(f => f.Seances)
                .ThenInclude(s => s.ContenuDans)
                    .ThenInclude(r => r.ContientDans)
            .Include(f => f.Seances)
                .ThenInclude(s => s.Salle)
            .AsSplitQuery()
            .FirstOrDefaultAsync(f => f.id_film == id_film);
    }
    
    public async Task AddAsync(Film film)
    {
        await context.Films.AddAsync(film);
        await context.SaveChangesAsync();
    }
}