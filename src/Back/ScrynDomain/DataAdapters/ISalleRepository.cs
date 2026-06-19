using System.Linq.Expressions;
using ScrynDomain.Entities;

namespace ScrynDomain.DataAdapters;

public interface ISalleRepository: IRepository<Salle>
{
    public Task<Salle?> FindSalleComplet(long id_salle);
}