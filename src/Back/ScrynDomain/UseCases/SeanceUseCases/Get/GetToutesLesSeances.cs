using ScrynDomain.Entities;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.SeanceUseCase.Get;

public class GetToutesLesSeances (IRepositoryFactory repositoryFactory)
{
    public async Task<List<Seance>?> ExecuteAsync()
    {
        await CheckBusinessRules();
        List<Seance>? seances = await repositoryFactory.SeanceRepository().FindAllAsync();
        return seances;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ISeanceRepository seanceRepository=repositoryFactory.SeanceRepository();
        ArgumentNullException.ThrowIfNull(seanceRepository);
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.Cineaste) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}