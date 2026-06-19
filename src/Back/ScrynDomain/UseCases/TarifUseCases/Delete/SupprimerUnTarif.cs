using System.ComponentModel.Design;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.Exceptions.SalleExceptions;

namespace ScrynDomain.UseCases.TarifUseCases.Delete;

public class SupprimerUnTarif(IRepositoryFactory repositoryFactory)
{

    public async Task ExecuteAsync(Tarif tarif)
    {
        await CheckBusinessRules(tarif);
        await repositoryFactory.TarifRepository().DeleteAsync(tarif);
        await repositoryFactory.SaveChangesAsync();
    }

    private async Task CheckBusinessRules(Tarif tarif)
    {
        ArgumentNullException.ThrowIfNull(tarif);
        ArgumentNullException.ThrowIfNull(repositoryFactory.TarifRepository());

        var tarifs = await repositoryFactory.TarifRepository()
            .FindByConditionAsync(t => t.id_tarif.Equals(tarif.id_tarif));

        if (!tarifs.Any()) throw new TarifDoesntExistException();
    }
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}