using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;

namespace ScrynDomain.UseCases.UserUseCases;

public class GetAllUsersUseCase
{
    private readonly IRepositoryFactory _repositoryFactory;

    public GetAllUsersUseCase(IRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
    }
    

    public async Task<IEnumerable<IUtilisateur>> ExecuteAsync()
    {
        var repo = _repositoryFactory.ScrynUser();
        return await repo.GetAllUsersAsync(); 
    }
}
