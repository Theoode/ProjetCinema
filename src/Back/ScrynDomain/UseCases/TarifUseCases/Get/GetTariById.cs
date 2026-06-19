using Microsoft.EntityFrameworkCore.Metadata;
using ScrynDomain.Entities;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.TarifUseCases.Get;

public class GetTariById(IRepositoryFactory repositoryFactory)
{
    public async Task<Tarif?> ExecuteAsync(long idTarif)
    {
        await CheckBusinessRules();
        Tarif? tarif = await repositoryFactory.TarifRepository().FindAsync(idTarif);
        return tarif;
    }


    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ITarifRepository tarifRepository = repositoryFactory.TarifRepository();
        ArgumentNullException.ThrowIfNull(tarifRepository);
    }
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.Cineaste) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
    //Ecrire IsAuthorized plus tard.
}