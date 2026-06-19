using Microsoft.EntityFrameworkCore;
using ScrynDataProvider.Repositories;
using ScrynDomain.DataAdapters;
using ScrynDomain.Entities;
using WebApplication1.Data;

namespace Scryn.Repositories;

public class TarifRepository(ScrynDbContext context) : Repository<Tarif>(context), ITarifRepository
{
        public Task<Tarif?> FindTarifComplet(long idTarif)
        {
                ArgumentNullException.ThrowIfNull(context.Tarifs);
                return context.Tarifs.Include(t => t.AppliqueDans)
                        .FirstOrDefaultAsync(tarif => tarif.id_tarif.Equals(idTarif));
        }
}