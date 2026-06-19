using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.Exceptions.FilmExceptions;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Exceptions.SalleExceptions;

namespace ScrynDomain.UseCases.SalleUseCases.Create;

public class AjouterUneSalle (IRepositoryFactory repositoryFactory)
{
    public async Task<Salle>? ExecuteAsync(long numeroSalle, long capacite, bool disponibiliteSalle, string type)
    {
        var salle = new Salle
        {
           numero_salle = numeroSalle,
           capacite = capacite,
           disponibilite_salle = disponibiliteSalle, 
           type = type 
        };
        return await ExecuteAsync(salle);
    }
    public async Task<Salle>? ExecuteAsync(Salle salle)
    {
        CheckBusinessRules(salle);
        Salle salleRe = await repositoryFactory.SalleRepository().CreateAsync(salle);
        repositoryFactory.SalleRepository().SaveChangesAsync().Wait();
        return salleRe;
    }

    private async Task CheckBusinessRules(Salle salle)
    {
        ArgumentNullException.ThrowIfNull(salle);
        ArgumentNullException.ThrowIfNull(repositoryFactory.SalleRepository());
        ArgumentNullException.ThrowIfNull(salle.numero_salle);
        ArgumentNullException.ThrowIfNull(salle.capacite);
        
        List<Salle>? salles = await repositoryFactory.SalleRepository().FindByConditionAsync(s => s.id_salle.Equals(salle.id_salle));
        if(salles.Any()) throw
        new SalleAlreadyExistException("Cette salle existe déjà dans le catalogue");
    }

    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Directeur) || role.Equals(Roles.DirecteurTechnique)|| role.Equals(Roles.Employe);
    }
    //Ecrire fonction de rôle ici.
    
}