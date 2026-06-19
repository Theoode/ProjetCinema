using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.FilmUseCases.Get;

public class GetToutesLesSalles(IRepositoryFactory repositoryFactory)
{
    public async Task<List<Salle>?> ExecuteAsync()
    {
        await CheckBusinessRules();
        List<Salle>? salles = await repositoryFactory.SalleRepository().FindAllAsync();
        return salles;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ISalleRepository salleRepository=repositoryFactory.SalleRepository();
        ArgumentNullException.ThrowIfNull(salleRepository);
    }
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Employe);
    }
}