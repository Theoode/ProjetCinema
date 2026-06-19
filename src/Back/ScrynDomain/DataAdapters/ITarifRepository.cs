using System.Linq.Expressions;
using ScrynDomain.Entities;

namespace ScrynDomain.DataAdapters;

public interface ITarifRepository: IRepository<Tarif>
{
    public Task<Tarif?> FindTarifComplet(long idTarif);
}