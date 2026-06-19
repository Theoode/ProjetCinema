using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.FilmUseCases.Get;

public class GetSalleById(IRepositoryFactory repositoryFactory)
{
    public async Task<Salle?> ExecuteAsync(long idSalle)
    {
        await CheckBusinessRules();
        Salle? salle = await repositoryFactory.SalleRepository().FindAsync(idSalle);
        return salle;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ArgumentNullException.ThrowIfNull(repositoryFactory.SalleRepository());
    }

    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Directeur) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Employe);
    }
}
