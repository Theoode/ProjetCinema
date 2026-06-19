using ScrynDomain.Entities;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.TarifUseCases.Get;

public class GetTarifCompletById(IRepositoryFactory repositoryFactory)
{
    public async Task<Tarif?> ExecuteAsync(long idTarif)
    {
        await CheckBusinessRules();
        Tarif? tarif = await repositoryFactory.TarifRepository().FindTarifComplet(idTarif); //Ecrire FindFilmComplet
        return tarif;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ArgumentNullException.ThrowIfNull(repositoryFactory.TarifRepository());
    }
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.Cineaste) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
    //Ecrire IsAuthorized plus tard.
}
