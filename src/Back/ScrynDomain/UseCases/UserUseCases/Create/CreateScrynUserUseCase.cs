using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;

namespace ScrynDomain.UseCases.UserUseCases;

public class CreateScrynUserUseCase(IRepositoryFactory factory)
{
    public async Task<IUtilisateur?> ExecuteAsync(string email, string userName, string password, string role )
    {
        await CheckBusinessRules(userName, password, role);
        IUtilisateur? userCree = await factory.ScrynUser().AddUserAsync(email, userName, password, role);
        await factory.SaveChangesAsync();
        return userCree;
    }
    private async Task CheckBusinessRules(string userName, string password, string role)
    {
        ArgumentNullException.ThrowIfNull(userName);
        ArgumentNullException.ThrowIfNull(password);
        ArgumentNullException.ThrowIfNull(role);
        ArgumentNullException.ThrowIfNull(factory);
    }

}