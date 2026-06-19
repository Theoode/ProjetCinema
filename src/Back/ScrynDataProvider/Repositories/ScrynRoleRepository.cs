using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using ScrynDataProvider.Entities;
using ScrynDomain.DataAdapters;
using ScrynDomain.Entities;
using WebApplication1.Data;

namespace ScrynDataProvider.Repositories;

public class ScrynRoleRepository(ScrynDbContext context, RoleManager<ScrynRole> roleManager) : Repository<IRole>(context), IScrynRoleRepository
{
    public async Task AddRoleAsync(string role)
    { 
        await roleManager.CreateAsync(new ScrynRole(role));
    }
    
}