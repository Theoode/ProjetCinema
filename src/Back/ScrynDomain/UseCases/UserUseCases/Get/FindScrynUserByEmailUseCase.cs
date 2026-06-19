using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;

namespace ScrynDomain.UseCases.UserUseCases;

public class FindScrynUserByEmailUseCase(IRepositoryFactory factory)
{
    public async Task<IUtilisateur?> ExecuteAsync(string email)
    {
        await CheckBusinessRules(email);
        IUtilisateur? user = await factory.ScrynUser().FindByEmailAsync(email);
        return user;
    }

    private async Task CheckBusinessRules(string email)
    {
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(factory);
        ArgumentNullException.ThrowIfNull(factory.ScrynUser());
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Directeur) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Employe) || role.Equals(Roles.Employe);
    }
}
