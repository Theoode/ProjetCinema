using ScrynDomain.Entities;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.PlaceUseCases.Get;

public class GetToutesLesPlaces(IRepositoryFactory repositoryFactory)
{
    /**
     * Récupérer toutes les places d'une salle, utile pour afficher la salle complète dans le front.
     */
    public async Task<List<Place>?> ExecuteAsync()
    {
        await CheckBusinessRules();
        List<Place>? places = await repositoryFactory.PlaceRepository().FindAllAsync();
        return places;
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