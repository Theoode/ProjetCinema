using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;

public class ModifierInfoPlace(IRepositoryFactory repositoryFactory) {

/**
 * Permet de modifier les informations d'une place, a quelle salle elle fait partie etc...
 */
    public async Task ExecuteAsync(Place place, long numero_place, bool disponibilite, Salle faitPartie, Reservation reservation){
        
        var placeUpdate = await repositoryFactory.PlaceRepository().FindAsync(place.id_place);
        await CheckBusinessRules(placeUpdate);
        placeUpdate.numero_place = numero_place;
        placeUpdate.disponibilite = disponibilite;
        placeUpdate.FaitPartie = faitPartie;
        placeUpdate.Reservation = reservation;

        
       await repositoryFactory.PlaceRepository().UpdateAsync(placeUpdate);
       await repositoryFactory.PlaceRepository().SaveChangesAsync();
    }

    private async Task CheckBusinessRules(Place place){
        ArgumentNullException.ThrowIfNull(place, nameof(place));
        ArgumentNullException.ThrowIfNull(repositoryFactory.PlaceRepository());
        //suite à réfléchir
    }
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe)|| role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}