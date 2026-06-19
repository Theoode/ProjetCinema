using System.ComponentModel.Design;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.Exceptions.SalleExceptions;
using ScrynDomain.Exceptions.SeanceExceptions;

namespace ScrynDomain.UseCases.SeanceUseCase.Delete;

public class SupprimerSeance(IRepositoryFactory repositoryFactory)
{

    public async Task ExecuteAsync(Seance seance)
    {
        await CheckBusinessRules(seance);
        await repositoryFactory.SeanceRepository().DeleteAsync(seance);
        await repositoryFactory.SaveChangesAsync();
    }

    private async Task CheckBusinessRules(Seance seance)
    {
        ArgumentNullException.ThrowIfNull(seance);
        ArgumentNullException.ThrowIfNull(repositoryFactory.SeanceRepository());

        var seances = await repositoryFactory.SeanceRepository()
            .FindByConditionAsync(s => s.id_seance.Equals(seance.id_seance));

        if (!seances.Any()) throw new SeanceDoesntExistException();
    }
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}