using Microsoft.EntityFrameworkCore;
using ScrynDataProvider.Repositories;
using ScrynDomain.DataAdapters;
using ScrynDomain.Entities;
using WebApplication1.Data;

namespace Scryn.Repositories;

public class PaiementRepository(ScrynDbContext context) : Repository<Paiement>(context), IPaiementRepository
{
    public Task<Paiement?> FindPaiementComplet(long idPaiement)
    {
        return context.Paiement
            .Include(p =>p.Reservation)
            .FirstOrDefaultAsync(p => p.id_paiement == idPaiement);
    }
}