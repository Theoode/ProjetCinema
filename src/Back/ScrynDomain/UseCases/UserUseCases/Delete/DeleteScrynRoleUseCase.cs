using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;

namespace ScrynDomain.UseCases.RoleUseCases.Delete;

public class DeleteScrynRoleUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task ExecuteAsync(string mail,Roles role)
    {
        await CheckBusinessRules(mail,role);
        await repositoryFactory.ScrynUser().RemoveRoleAsync(mail,role);
        await repositoryFactory.SaveChangesAsync();
    }

    private async Task CheckBusinessRules(string mail,Roles role)
    {
        ArgumentNullException.ThrowIfNull(role);
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ArgumentNullException.ThrowIfNull(mail);

    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Directeur) || role.Equals(Roles.DirecteurTechnique);
    }
}