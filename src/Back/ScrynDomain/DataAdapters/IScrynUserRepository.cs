using ScrynDomain.Entities;

namespace ScrynDomain.DataAdapters;

public interface IScrynUserRepository: IRepository<IUtilisateur>
{
    Task<IUtilisateur?> AddUserAsync(string login, string email, string password, string role);
    Task<IUtilisateur> FindByEmailAsync(string email);
    
    Task<IEnumerable<IUtilisateur>> GetAllUsersAsync();

    Task<bool> IsInRoleAsync(string email, string role);

    Task<IUtilisateur> UpdateAsync(IUtilisateur user);
    Task RemoveRoleAsync(string mail, Roles role);
}