using ScrynDomain.Entities;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.SeanceUseCase.Get;

public class GetSeanceCompletById(IRepositoryFactory repositoryFactory)
{
    public async Task<Seance?> ExecuteAsync(long idSeance)
    {
        await CheckBusinessRules();
        Seance? seance = await repositoryFactory.SeanceRepository().FindSeanceComplet(idSeance);
        return seance;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ArgumentNullException.ThrowIfNull(repositoryFactory.SeanceRepository());
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.Cineaste) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
    
}
