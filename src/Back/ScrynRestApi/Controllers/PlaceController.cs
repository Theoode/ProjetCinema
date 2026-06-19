using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Dtos;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.PlaceUseCases.Create;
using ScrynDomain.UseCases.PlaceUseCases.Delete;
using ScrynDomain.UseCases.PlaceUseCases.Get;
using ScrynDomain.UseCases.UserUseCases;
using ScrynDomain.UseCases.RoleUseCases.Get;

namespace ScrynRestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PlaceController(IRepositoryFactory repositoryFactory) : ControllerBase
{
    [HttpPost("{id}")]
    public async Task<ActionResult<PlaceDto>> ChoisirPlace(long id)
    {
        ChoisirPlace _choisirPlace = new ChoisirPlace(repositoryFactory);
        string role = "";
        string email = "";
        IUtilisateur user = null;

        try
        {
            CheckSecu(out role, out email, out user);
        }
        catch
        {
            return Unauthorized();
        }

        if (!_choisirPlace.IsAuthorized(role)) return Unauthorized();

        var place = await _choisirPlace.ExecuteAsync(id);
        var dto = new PlaceDto().ToDto(place);
        return Ok(dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PlaceDto>> GetPlace(long id)
    {
        GetPlaceById _getPlaceById = new GetPlaceById(repositoryFactory);

        var place = await _getPlaceById.ExecuteAsync(id);
        if (place == null) return NotFound("Place non trouvée.");

        PlaceDto dto = new PlaceDto().ToDto(place);
        return Ok(dto);
    }

    [HttpGet("complet/{id}")]
    public async Task<ActionResult<PlaceDto>> GetPlaceComplet(long id)
    {
        GetPlaceCompletById _getPlaceCompletById = new GetPlaceCompletById(repositoryFactory);
        string role = "";
        string email = "";
        IUtilisateur user = null;

        try
        {
            CheckSecu(out role, out email, out user);
        }
        catch
        {
            return Unauthorized();
        }

        if (!_getPlaceCompletById.IsAuthorized(role)) return Unauthorized();

        var place = await _getPlaceCompletById.ExecuteAsync(id);
        if (place == null) return NotFound("Place complète non trouvée.");

        var dto = new PlaceDto().ToDto(place);
        return Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlaceDto>>> GetAllPlaces()
    {
        GetToutesLesPlaces _getToutesLesPlaces = new GetToutesLesPlaces(repositoryFactory);
        string role = "";
        string email = "";
        IUtilisateur user = null;

        try
        {
            CheckSecu(out role, out email, out user);
        }
        catch
        {
            return Unauthorized();
        }

        if (!_getToutesLesPlaces.IsAuthorized(role)) return Unauthorized();

        var places = await _getToutesLesPlaces.ExecuteAsync();
        var dtos = places.Select(p => new PlaceDto().ToDto(p)).ToList();

        return Ok(dtos);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SupprimerPlace(long id)
    {
        GetPlaceById _getPlaceById = new GetPlaceById(repositoryFactory);
        SupprimerPlace _supprimerPlace = new SupprimerPlace(repositoryFactory);
        string role = "";
        string email = "";
        IUtilisateur user = null;

        try
        {
            CheckSecu(out role, out email, out user);
        }
        catch
        {
            return Unauthorized();
        }

        if (!_getPlaceById.IsAuthorized(role) || !_supprimerPlace.IsAuthorized(role)) return Unauthorized();

        var place = await _getPlaceById.ExecuteAsync(id);
        if (place == null) return NotFound("Place non trouvée.");

        await _supprimerPlace.ExecuteAsync(place);
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