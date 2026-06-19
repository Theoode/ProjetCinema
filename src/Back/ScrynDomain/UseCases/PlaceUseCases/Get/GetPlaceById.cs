using ScrynDomain.Entities;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.PlaceUseCases.Get;

public class GetPlaceById(IRepositoryFactory repositoryFactory)
{
    /**
     * Récupérer une place avec l'ID
     */
    public async Task<Place?> ExecuteAsync(long idPlace)
    {
        await CheckBusinessRules();
        Place? place = await repositoryFactory.PlaceRepository().FindAsync(idPlace);
        return place;
    }


    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        IPlaceRepository placeRepository = repositoryFactory.PlaceRepository();
        ArgumentNullException.ThrowIfNull(placeRepository);
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.Cineaste) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}