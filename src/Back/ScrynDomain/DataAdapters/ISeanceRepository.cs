using ScrynDomain.Entities;

namespace ScrynDomain.DataAdapters;

public interface ISeanceRepository: IRepository<Seance>
{
    public Task<Seance?> FindSeanceComplet(long idSeance);
    Task AddAsync(Seance seance);
}