using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;

public class ModifierInfoSalle(IRepositoryFactory repositoryFactory) {


    public async Task ExecuteAsync(Salle salle, long numero_salle, long capacite, bool disponibilite_salle, string type){
        
        var salleUpdate = await repositoryFactory.SalleRepository().FindAsync(salle.id_salle);
        await CheckBusinessRules(salleUpdate);
        salleUpdate.numero_salle = numero_salle;
        salleUpdate.capacite = capacite;
        salleUpdate.disponibilite_salle = disponibilite_salle;
        salleUpdate.type = type;

        
       await repositoryFactory.SalleRepository().UpdateAsync(salleUpdate);
       await repositoryFactory.SalleRepository().SaveChangesAsync();
    }

    private async Task CheckBusinessRules(Salle salle){
        ArgumentNullException.ThrowIfNull(salle, nameof(salle));
        ArgumentNullException.ThrowIfNull(repositoryFactory.SalleRepository());
        //suite à réfléchir
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Employe);
    }

}