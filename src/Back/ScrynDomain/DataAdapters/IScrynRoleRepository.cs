using ScrynDomain.Entities;

namespace ScrynDomain.DataAdapters;

public interface IScrynRoleRepository : IRepository<IRole>
{
    public Task AddRoleAsync(string role);

}