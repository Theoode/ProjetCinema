using ScrynDomain.Entities;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.PaiementUseCases.Get;

public class GetPaiementCompletById(IRepositoryFactory repositoryFactory)
{
    /**
     * Récupérer une place et tout ce qu'il inclut, en d'autres termes dans quelle salle est la place etc....
     */
    public async Task<Paiement?> ExecuteAsync(long idPaiement)
    {
        await CheckBusinessRules();
        Paiement? paiement = await repositoryFactory.PaiementRepository().FindPaiementComplet(idPaiement); //Ecrire FindFilmComplet
        return paiement;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ArgumentNullException.ThrowIfNull(repositoryFactory.PaiementRepository());
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.Cineaste) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}
