using ScrynDomain.Entities;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.TarifUseCases.Get;

public class GetTousLesTarifs(IRepositoryFactory repositoryFactory)
{
    public async Task<List<Tarif>?> ExecuteAsync()
    {
        await CheckBusinessRules();
        List<Tarif>? tarifs = await repositoryFactory.TarifRepository().FindAllAsync();
        return tarifs;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        IPlaceRepository placeRepository=repositoryFactory.PlaceRepository();
        ArgumentNullException.ThrowIfNull(placeRepository);
    }
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.Cineaste) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}