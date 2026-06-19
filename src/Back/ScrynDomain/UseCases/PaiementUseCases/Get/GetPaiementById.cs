using ScrynDomain.Entities;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.PlaceUseCases.Get;

public class GetPaiementById(IRepositoryFactory repositoryFactory)
{
    /**
     * Récupérer une place avec l'ID
     */
    public async Task<Paiement?> ExecuteAsync(long idPlace)
    {
        await CheckBusinessRules();
        Paiement? paiement = await repositoryFactory.PaiementRepository().FindAsync(idPlace);
        return paiement;
    }


    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        IPaiementRepository paiementRepository = repositoryFactory.PaiementRepository();
        ArgumentNullException.ThrowIfNull(paiementRepository);
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.Cineaste) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}