using Microsoft.AspNetCore.Identity;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.Exceptions.UserExceptions;

namespace ScrynDomain.UseCases.SecurityUseCases.Create;

public class AffectScrynRoleUseCase(UserManager<IUtilisateur> userManager,IRepositoryFactory repositoryFactory)
{
    public async Task<bool> ExecuteAsync(string mail, string role)
    {
        var user = await userManager.FindByEmailAsync(mail);
        if (user == null)
        {
            throw new UserNotFoundException("Utilisateur non trouvé");
        }
        var result = await userManager.AddToRoleAsync(user, role);
        return result.Succeeded;
    }

    private async Task CheckBusinessRules(string mail, string role)
    {
        ArgumentNullException.ThrowIfNull(mail);
        ArgumentNullException.ThrowIfNull(role);
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ArgumentNullException.ThrowIfNull(repositoryFactory.ScrynUser());
        ArgumentNullException.ThrowIfNull(repositoryFactory.ScrynRole());
    }

    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}