using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;

namespace ScrynDomain.UseCases.RoleUseCases.Get;

public class IsInRoleUseCase(IRepositoryFactory factory)
{
    public async Task<bool> ExecuteAsync(string email, string role)
    {
        await CheckBusinessRules(email);
        return await factory.ScrynUser().IsInRoleAsync(email, role);
    }

    private async Task CheckBusinessRules(string email)
    {
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(factory);
        ArgumentNullException.ThrowIfNull(factory.ScrynUser());
    }
  
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Directeur) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Cineaste);
    }
}

