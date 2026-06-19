using System.Runtime.InteropServices.JavaScript;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;

public class ModifierInfoTarif(IRepositoryFactory repositoryFactory) {


    public async Task ExecuteAsync(string nom_tarif, float valeur, DateTime date_deb, DateTime date_fin, List<Seance>? AppliqueDans, Tarif tarif){
        
        var tarifUpdate = await repositoryFactory.TarifRepository().FindAsync(tarif.id_tarif);
        await CheckBusinessRules(tarifUpdate);
        tarifUpdate.nom_tarif = nom_tarif;
        tarifUpdate.valeur = valeur;
        tarifUpdate.date_deb = date_deb;
        tarifUpdate.date_fin = date_fin;
        tarifUpdate.AppliqueDans = AppliqueDans;
        

        
       await repositoryFactory.TarifRepository().UpdateAsync(tarifUpdate);
       await repositoryFactory.SalleRepository().SaveChangesAsync();
    }

    private async Task CheckBusinessRules(Tarif  tarif){
        ArgumentNullException.ThrowIfNull(tarif, nameof(tarif));
        ArgumentNullException.ThrowIfNull(repositoryFactory.TarifRepository());
        //suite à réfléchir
    }
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}