using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.Exceptions.PlaceExceptions;

namespace ScrynDomain.UseCases.PlaceUseCases.Create;

public class ChoisirPlace (IRepositoryFactory repositoryFactory)
{
    /**
     * Choix d'une place dans une salle, les places sont créées par défaut
     * , cette fonction permet seulement de mettre a faux la disponibilité de la place.
     */
    public async Task<Place> ExecuteAsync(long id_place)
    {
        var place = repositoryFactory.PlaceRepository().FindAsync(id_place);
        place.Result.disponibilite = false;
        await repositoryFactory.PlaceRepository().UpdateAsync(place.Result);
        await repositoryFactory.SaveChangesAsync();
        return place.Result;
    }
    

    private async Task CheckBusinessRules(Place place)
    {
        ArgumentNullException.ThrowIfNull(place);
        ArgumentNullException.ThrowIfNull(repositoryFactory.PlaceRepository());
        ArgumentNullException.ThrowIfNull(place.numero_place);
        ArgumentNullException.ThrowIfNull(place.disponibilite);
        
        List<Place>? places = await repositoryFactory.PlaceRepository().FindByConditionAsync(p => p.id_place.Equals(place.id_place));
        if(places.Any()) throw
            new PlaceAlreadyExistException("Cette place existe déjà dans la liste");

        var placedispo = await repositoryFactory.PlaceRepository().FindAsync(place.id_place);

        if (placedispo.disponibilite = false)
        {
            throw new PlaceNonDispoException("Cette place est déja prise dans une réservation");
        }
    }

    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.Cineaste) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}