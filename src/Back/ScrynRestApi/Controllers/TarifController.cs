using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ScrynApplication.UseCases.FilmUseCases.Create;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Dtos;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.FilmUseCases.Create;
using ScrynDomain.UseCases.FilmUseCases.Delete;
using ScrynDomain.UseCases.FilmUseCases.Get;
using ScrynDomain.UseCases.RoleUseCases.Get;
using ScrynDomain.UseCases.TarifUseCases.Get;
using ScrynDomain.UseCases.UserUseCases;

namespace ScrynRestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarifController(IRepositoryFactory repositoryFactory) : ControllerBase
{

    [HttpGet("{id}")]
    public async Task<ActionResult<FilmDto>> GetFilm(long id)
    {
        GetTariById _getTarifId = new GetTariById(repositoryFactory);

        var tarif = await _getTarifId.ExecuteAsync(id);
        if (tarif == null) return NotFound("Film non trouvé.");

        TarifDto dto = new TarifDto().ToDto(tarif);
        return Ok(dto);
    }


    [HttpGet("complet/{id}")]
    public async Task<ActionResult<FilmDto>> GetTarifComplet(long id)
    {
        GetTarifCompletById _getTarifCompletById = new GetTarifCompletById(repositoryFactory);
    /*    string role = "";
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

        if (!_getFilmCompletById.IsAuthorized(role)) return Unauthorized();*/
        var tarif = await _getTarifCompletById.ExecuteAsync(id);
        if (tarif == null) return NotFound("Film complet non trouvé.");
     
        TarifDto dto = new TarifDto().ToDto(tarif);
        return Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FilmDto>>> GetAllTarifs()
    {
        GetTousLesTarifs _getAllTarifs = new GetTousLesTarifs(repositoryFactory);
     /*   string role = "";
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

        if (!_getAllFilms.IsAuthorized(role)) return Unauthorized();*/
        var tarifs = await _getAllTarifs.ExecuteAsync();
        var dtos = tarifs.Select(f => new TarifDto().ToDto(f)).ToList();

        return Ok(dtos);
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