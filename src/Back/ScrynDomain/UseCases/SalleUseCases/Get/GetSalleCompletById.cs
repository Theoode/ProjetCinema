using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;

namespace ScrynDomain.UseCases.SalleUseCases.Get;

public class GetSalleCompletById(IRepositoryFactory repositoryFactory)
{
    public async Task<Salle?> ExecuteAsync(long idSalle)
    {
        await CheckBusinessRules();
        Salle? salle = await repositoryFactory.SalleRepository().FindSalleComplet(idSalle); 
        return salle;
    }


    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ISalleRepository salleRepository = repositoryFactory.SalleRepository();
        ArgumentNullException.ThrowIfNull(salleRepository);
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Directeur) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Employe);
    }
}