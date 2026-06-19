using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Dtos;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.RoleUseCases.Get;
using ScrynDomain.UseCases.SeanceUseCase.Create;
using ScrynDomain.UseCases.SeanceUseCase.Delete;
using ScrynDomain.UseCases.SeanceUseCase.Get;
using ScrynDomain.UseCases.SeanceUseCase.Update;
using ScrynDomain.UseCases.UserUseCases;


namespace ScrynRestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SeanceController(IRepositoryFactory repositoryFactory) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateSeance([FromBody] CreateSeanceDto dto)
    {
        if (dto == null)
            return BadRequest("Les informations de la séance sont requises.");

        string role = "";
        string email = "";
        IUtilisateur user = null;

        /*try
        {
            CheckSecu(out role, out email, out user);
        }
        catch (Exception)
        {
            return Unauthorized();
        }*/

        var seanceRepo = repositoryFactory.CreateSeanceRepository();
        var createSeanceUseCase = new CreateSeanceUseCase(seanceRepo);

        await createSeanceUseCase.ExecuteAsync(dto);

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SeanceDto>> GetSeance(long id)
    {
        GetFilmBySeance _getFilmBySeance = new GetFilmBySeance(repositoryFactory);
        var seance = await _getFilmBySeance.ExecuteAsync(id);

        if (seance == null) return NotFound("Séance non trouvée.");

        var dto = new SeanceDto().ToDto(seance);
        return Ok(dto);
    }

    [HttpGet("complet/{id}")]
    public async Task<ActionResult<SeanceDto>> GetSeanceComplet(long id)
    {
        GetSeanceCompletById _getComplet = new GetSeanceCompletById(repositoryFactory);
        var seance = await _getComplet.ExecuteAsync(id);

        if (seance == null) return NotFound("Séance complète non trouvée.");

        var dto = new SeanceDto().ToDto(seance);
        return Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SeanceDto>>> GetToutesLesSeances()
    {
        GetToutesLesSeances _getAll = new GetToutesLesSeances(repositoryFactory);
        var seances = await _getAll.ExecuteAsync();

        var dtos = seances?.Select(s => new SeanceDto().ToDto(s)).ToList();
        return Ok(dtos);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ModifierSeance(long id, [FromBody] SeanceDto seanceModifiee)
    {
        GetFilmBySeance _getSeance = new GetFilmBySeance(repositoryFactory);
        var seance = await _getSeance.ExecuteAsync(id);

        if (seance == null) return NotFound("Séance non trouvée.");

        ModifierInfoSeance _modifierInfoSeance = new ModifierInfoSeance(repositoryFactory);
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

        if (!_modifierInfoSeance.IsAuthorized(role)) return Unauthorized();
        await _modifierInfoSeance.ExecuteAsync(
            seance,
            seanceModifiee.date_seance,
            TarifDto.ToEntities(seanceModifiee.AppliqueSur),
            ReservationDto.ToEntities(seanceModifiee.ContenuDans),
            seanceModifiee.Film?.ToEntity(),
            seanceModifiee.Salle?.ToEntity()
        );

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SupprimerSeance(long id)
    {
        GetFilmBySeance _getSeance = new GetFilmBySeance(repositoryFactory);
        var seance = await _getSeance.ExecuteAsync(id);

        if (seance == null) return NotFound("Séance non trouvée.");

        SupprimerSeance _supprimerSeance = new SupprimerSeance(repositoryFactory);
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

        if (!_supprimerSeance.IsAuthorized(role)) return Unauthorized();
        await _supprimerSeance.ExecuteAsync(seance);
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
