using System.ComponentModel.Design;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.Exceptions.PlaceExceptions;
using ScrynDomain.Exceptions.SalleExceptions;

namespace ScrynDomain.UseCases.PlaceUseCases.Delete;

public class SupprimerPlace(IRepositoryFactory repositoryFactory)
{
/**
 * Permet la suppression d'une place, tous les employés peuvent utiliser cette fonction.
 */
    public async Task ExecuteAsync(Place place)
    {
        await CheckBusinessRules(place);
        await repositoryFactory.PlaceRepository().DeleteAsync(place);
        await repositoryFactory.SaveChangesAsync();
    }

    private async Task CheckBusinessRules(Place place)
    {
        ArgumentNullException.ThrowIfNull(place);
        ArgumentNullException.ThrowIfNull(repositoryFactory.PlaceRepository());

        var places = await repositoryFactory.PlaceRepository()
            .FindByConditionAsync(f => f.id_place.Equals(place.id_place));

        if (!places.Any()) throw new PlaceDoesntExistException();
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}