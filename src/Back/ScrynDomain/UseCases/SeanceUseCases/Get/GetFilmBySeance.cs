using ScrynDomain.Entities;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.SeanceUseCase.Get;

public class GetFilmBySeance(IRepositoryFactory repositoryFactory)
{
    public async Task<Seance?> ExecuteAsync(long idSeance)
    {
        await CheckBusinessRules();
        Seance? seance = await repositoryFactory.SeanceRepository().FindAsync(idSeance);
        return seance;
    }


    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ISeanceRepository seanceRepository = repositoryFactory.SeanceRepository();
        ArgumentNullException.ThrowIfNull(seanceRepository);
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.Cineaste) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
    
}