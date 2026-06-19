using ScrynDomain.Entities;

namespace ScrynDomain.DataAdapters;

public interface IPaiementRepository : IRepository<Paiement>
{
    public Task<Paiement?> FindPaiementComplet(long idPaiement);

}