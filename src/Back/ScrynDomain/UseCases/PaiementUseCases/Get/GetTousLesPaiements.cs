using ScrynDomain.Entities;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.PlaceUseCases.Get;

public class GetTousLesPaiements(IRepositoryFactory repositoryFactory)
{
    /**
     * Récupérer toutes les places d'une salle, utile pour afficher la salle complète dans le front.
     */
    public async Task<List<Paiement>?> ExecuteAsync()
    {
        await CheckBusinessRules();
        List<Paiement>? paiements = await repositoryFactory.PaiementRepository().FindAllAsync();
        return paiements;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        IPaiementRepository paiementRepository=repositoryFactory.PaiementRepository();
        ArgumentNullException.ThrowIfNull(paiementRepository);
    }
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.Cineaste) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}