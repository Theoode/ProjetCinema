

using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.SecurityUseCases.Create;
using ScrynDomain.UseCases.UserUseCases;

namespace ScrynDomain.Data;

public class BasicBdBuilder(IRepositoryFactory repositoryFactory) : BdBuilder(repositoryFactory)
{
    private readonly string Password = "Miage2025#";
 
    
    protected override async Task BuildRolesAsync()
    {
        // Création des rôles dans la table aspnetroles
        await new CreateScrynRoleUseCase(repositoryFactory).ExecuteAsync(Roles.Directeur);
        await new CreateScrynRoleUseCase(repositoryFactory).ExecuteAsync(Roles.DirecteurTechnique);
        await new CreateScrynRoleUseCase(repositoryFactory).ExecuteAsync(Roles.Employe);
        await new CreateScrynRoleUseCase(repositoryFactory).ExecuteAsync(Roles.Cineaste);
    }
    protected override async Task BuildUsersAsync()
    {
        CreateScrynUserUseCase uc = new CreateScrynUserUseCase(repositoryFactory);
        await uc.ExecuteAsync("atatoz@gmail.com","raximex","Miage2025*",Roles.Directeur);
        
    }
}