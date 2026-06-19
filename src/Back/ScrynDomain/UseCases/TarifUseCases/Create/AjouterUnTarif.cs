using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;

using ScrynDomain.Exceptions.SalleExceptions;

namespace ScrynDomain.UseCases.TarifUseCases.Create;

public class AjouterUnTarif (IRepositoryFactory repositoryFactory)
{
    public async Task<Tarif> ExecuteAsync(string nom_tarif, float valeur, DateTime date_deb, DateTime date_fin, List<Seance>? appliqueDans)
    {
        var tarif = new Tarif
        {
            nom_tarif = nom_tarif,
            valeur = valeur,
            date_deb = date_deb,
            date_fin = date_fin,
            AppliqueDans = appliqueDans
        };
        return await ExecuteAsync(tarif);
    }
    public async Task<Tarif> ExecuteAsync(Tarif tarif)
    {
        CheckBusinessRules(tarif);
        Tarif tarifRe = await repositoryFactory.TarifRepository().CreateAsync(tarif);
        repositoryFactory.TarifRepository().SaveChangesAsync().Wait();
        return tarifRe;
    }

    private async Task CheckBusinessRules(Tarif tarif)
    {
        ArgumentNullException.ThrowIfNull(tarif);
        ArgumentNullException.ThrowIfNull(repositoryFactory.TarifRepository());
        ArgumentNullException.ThrowIfNull(tarif.valeur);
        ArgumentNullException.ThrowIfNull(tarif.date_deb);
        ArgumentNullException.ThrowIfNull(tarif.date_fin);
        
        List<Tarif>? tarifs = await repositoryFactory.TarifRepository().FindByConditionAsync(f => f.id_tarif.Equals(tarif.id_tarif));
        if(tarifs.Any()) throw
        new TarifAlreadyExistException("Ce film existe déjà dans le catalogue");
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) ||role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
    //Ecrire fonction de rôle ici.
    
}