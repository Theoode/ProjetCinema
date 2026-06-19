using System.ComponentModel.Design;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.Exceptions.SalleExceptions;

namespace ScrynDomain.UseCases.SalleUseCases.Delete;

public class SupprimerUneSalle(IRepositoryFactory repositoryFactory)
{

    public async Task ExecuteAsync(Salle salle)
    {
        await CheckBusinessRules(salle);
        await repositoryFactory.SalleRepository().DeleteAsync(salle);
        await repositoryFactory.SaveChangesAsync();
    }

    private async Task CheckBusinessRules(Salle salle)
    {
        ArgumentNullException.ThrowIfNull(salle);
        ArgumentNullException.ThrowIfNull(repositoryFactory.SalleRepository());

        var salles = await repositoryFactory.SalleRepository()
            .FindByConditionAsync(s => s.id_salle.Equals(salle.id_salle));

        if (!salles.Any()) throw new SalleDoesntExistException();
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Directeur) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Employe);
    }
}