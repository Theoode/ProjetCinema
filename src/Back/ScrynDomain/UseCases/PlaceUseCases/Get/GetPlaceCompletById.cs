using ScrynDomain.Entities;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.PlaceUseCases.Get;

public class GetPlaceCompletById(IRepositoryFactory repositoryFactory)
{
    /**
     * Récupérer une place et tout ce qu'il inclut, en d'autres termes dans quelle salle est la place etc....
     */
    public async Task<Place?> ExecuteAsync(long idPlace)
    {
        await CheckBusinessRules();
        Place? place = await repositoryFactory.PlaceRepository().FindPlaceComplet(idPlace); //Ecrire FindFilmComplet
        return place;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ArgumentNullException.ThrowIfNull(repositoryFactory.PlaceRepository());
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.Cineaste) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}
