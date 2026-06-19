using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.Exceptions.UserExceptions;
using ScrynDomain.UseCases.RoleUseCases.Delete;
using ScrynDomain.UseCases.UserUseCases;

namespace ScrynRestApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UserController(IRepositoryFactory repositoryFactory) : ControllerBase
{
    [HttpDelete("{mail}")]
    public async Task<IActionResult> SupprimerUser(string mail)
    {
        FindScrynUserByEmailUseCase _getUser = new FindScrynUserByEmailUseCase(repositoryFactory);
        var uc = _getUser.ExecuteAsync(mail);

        if (uc == null) throw new UserNotFoundException("non trouvé");
        
        DeleteScrynUserByEmailUseCase _deleteUser = new DeleteScrynUserByEmailUseCase(repositoryFactory);
    
        string role = "";
        string email = "";
        IUtilisateur user = null;
        try
        {
            CheckSecu(out role, out email, out user);
        }
        catch (Exception e)
        {
            return Unauthorized();
        }

        if (!_deleteUser.IsAuthorized(role)) return Unauthorized();
        await _deleteUser.ExecuteAsync(mail);
        return NoContent();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        GetAllUsersUseCase _getUsers = new GetAllUsersUseCase(repositoryFactory);

        string role = "";
        string email = "";
        IUtilisateur user = null;
     
        var users = await _getUsers.ExecuteAsync();
        return Ok(users);
    }
    
    
    
    [HttpDelete("Role/{mail}")]
    public async Task<IActionResult> SupprimerRoleToAUser(string mail,Roles role)
    {
        FindScrynUserByEmailUseCase _getUser = new FindScrynUserByEmailUseCase(repositoryFactory);
        var uc = _getUser.ExecuteAsync(mail);

        if (uc == null) throw new UserNotFoundException("non trouvé");
        
        DeleteScrynRoleUseCase _deleteRoleUser = new DeleteScrynRoleUseCase(repositoryFactory);
    
        string roleC = "";
        string email = "";
        IUtilisateur user = null;
        try
        {
            CheckSecu(out roleC, out email, out user);
        }
        catch (Exception e)
        {
            return Unauthorized();
        }

        if (!_deleteRoleUser.IsAuthorized(roleC)) return Unauthorized();
        await _deleteRoleUser.ExecuteAsync(mail, role);
        return NoContent();
    }
    
    
    
    private void CheckSecu(out string role, out string email, out IUtilisateur user)
    {
        role = "";
        ClaimsPrincipal claims = HttpContext.User;
        if (claims.FindFirst(ClaimTypes.Email)==null) throw new UnauthorizedAccessException();
        email = claims.FindFirst(ClaimTypes.Email).Value;
        if (email==null) throw new UnauthorizedAccessException();
        user = new FindScrynUserByEmailUseCase(repositoryFactory).ExecuteAsync(email).Result;
        if (user==null) throw new UnauthorizedAccessException();
        if (claims.Identity?.IsAuthenticated != true) throw new UnauthorizedAccessException();
        var ident = claims.Identities.FirstOrDefault();
        if (ident == null)throw new UnauthorizedAccessException();
        if (claims.FindFirst(ClaimTypes.Role)==null) throw new UnauthorizedAccessException();
        role = ident.FindFirst(ClaimTypes.Role).Value;
        if (role == null) throw new UnauthorizedAccessException();
    }
}