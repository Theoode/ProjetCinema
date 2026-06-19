using Microsoft.AspNetCore.Identity;
using ScrynDomain.Entities;
using ScrynDataProvider.Entities;
using ScrynDomain.DataAdapters;
using WebApplication1.Data;


namespace ScrynDataProvider.Repositories;

public class ScrynUserRepository(ScrynDbContext context, UserManager<ScrynUser> userManager, RoleManager<ScrynRole> roleManager) : Repository<IUtilisateur>(context), IScrynUserRepository
{
    public async Task<IUtilisateur?> AddUserAsync(string login, string email, string password, string role)
    {
        ScrynUser user = new ScrynUser { UserName = login, Email = email};
        IdentityResult result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, role);
        }
        await context.SaveChangesAsync();
        return result.Succeeded ? user : null;
        return user;
    }

    public async Task<IUtilisateur> FindByEmailAsync(string email)
    {
        return await userManager.FindByEmailAsync(email);
    }

    public async Task<IEnumerable<IUtilisateur>> GetAllUsersAsync()
    {
        var users = await Task.FromResult(userManager.Users.ToList());
        return users.Cast<IUtilisateur>();
    }

    
    public async Task UpdateAsync(IUtilisateur entity, string userName, string email)
    {
        ScrynUser user = (ScrynUser)entity;
        user.UserName = userName;
        user.Email = email;
        await userManager.UpdateAsync(user);
        await context.SaveChangesAsync();
    }
    public async Task<int> DeleteAsync(long id)
    {
        ScrynUser user= await userManager.FindByIdAsync(id.ToString());
        if (user!=null)
        {
            await userManager.DeleteAsync(user);
            int res=await  context.SaveChangesAsync();
            return 1;
        }
        return 0;
    }

    public async Task<bool> IsInRoleAsync(string email, string role)
    {
        bool res = false;
        var user =await userManager.FindByEmailAsync(email);
        return await userManager.IsInRoleAsync(user, role);
    }
    
    public async Task<IUtilisateur> UpdateAsync(IUtilisateur user)
    {
        context.Set<IUtilisateur>().Update(user);
        await SaveChangesAsync();
        return user;
    }

    public async Task RemoveRoleAsync(string mail, Roles role)
    {
        ScrynUser user = await userManager.FindByEmailAsync(mail);
        if (user != null)
        {
            userManager.RemoveFromRoleAsync(user, role.ToString());
            await SaveChangesAsync();
        }
    }
}