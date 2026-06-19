using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;

namespace ScrynDomain.UseCases.StatistiqueUseCases;

public class FilmStatistiqueUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<int> ExecuteAsync(Film film)
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory, nameof(repositoryFactory));
        var filmCount = repositoryFactory.ReservationRepository().FindByConditionAsync(reservation => reservation.Seance.Film.Equals(film)).Result.Count;
        return filmCount;
    }
    
}