using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;

namespace ScrynDomain.UseCases.UserUseCases;

public class DeleteScrynUserByEmailUseCase(IRepositoryFactory repositoryFactory)
{
    public async Task<bool> ExecuteAsync(string email)
    {
        await CheckBusinessRules(email);

        var userRepository = repositoryFactory.ScrynUser();
        var user = await userRepository.FindByEmailAsync(email);

        if (user == null)
            return false;

        await userRepository.DeleteAsync(user);
        return true;
    }

    private async Task CheckBusinessRules(string email)
    {
        ArgumentNullException.ThrowIfNull(email);
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        ArgumentNullException.ThrowIfNull(repositoryFactory.ScrynUser());
    }

    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Directeur) || role.Equals(Roles.DirecteurTechnique);
    }
}