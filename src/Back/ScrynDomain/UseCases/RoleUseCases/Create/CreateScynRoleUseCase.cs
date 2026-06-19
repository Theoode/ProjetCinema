using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;

namespace ScrynDomain.UseCases.SecurityUseCases.Create;

public class CreateScrynRoleUseCase(IRepositoryFactory factory)
{
    public async Task ExecuteAsync(string role)
    {
        await CheckBusinessRules(role);
        await factory.ScrynRole().AddRoleAsync(role);
        await factory.SaveChangesAsync();
    }

    private async Task CheckBusinessRules(string role)
    {
        ArgumentNullException.ThrowIfNull(role);
        ArgumentNullException.ThrowIfNull(factory);
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Directeur) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Employe); ;
    }
}