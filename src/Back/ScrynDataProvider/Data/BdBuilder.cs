using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.Data;

public abstract class BdBuilder(IRepositoryFactory repositoryFactory)
{
    public async Task BuildScrynBdAsync()
    {
       
        
        Console.WriteLine("BuildRoles");
        await BuildRolesAsync();
        await BuildUsersAsync();
  
    }

    protected abstract Task BuildRolesAsync();
    protected abstract Task BuildUsersAsync();
}