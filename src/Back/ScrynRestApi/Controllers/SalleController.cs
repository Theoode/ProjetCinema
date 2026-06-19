using Microsoft.AspNetCore.Mvc;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.SalleUseCases.Create;
using ScrynDomain.UseCases.SalleUseCases.Delete;
using System.Linq;
using System.Security.Claims;
using ScrynDomain.Dtos;
using ScrynDomain.UseCases.FilmUseCases.Get;
using ScrynDomain.UseCases.RoleUseCases.Get;
using ScrynDomain.UseCases.UserUseCases;

namespace ScrynRestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SalleController(IRepositoryFactory repositoryFactory) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<SalleDto>> AjouterSalle([FromBody] SalleDto salleDto)
    {
        if (salleDto == null) return BadRequest("Les informations de la salle sont requises.");

        var salle = salleDto.ToEntity();
        AjouterUneSalle _ajouterUneSalle = new AjouterUneSalle(repositoryFactory);
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

        if (!_ajouterUneSalle.IsAuthorized(role)) return Unauthorized();
        var result = await _ajouterUneSalle.ExecuteAsync(salle);

        var dto = new SalleDto().ToDto(result);
        return CreatedAtAction(nameof(GetSalle), new { id = dto?.id_salle }, dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SalleDto>> GetSalle(long id)
    {
        GetSalleById _getSalleById = new GetSalleById(repositoryFactory);
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

        if (!_getSalleById.IsAuthorized(role)) return Unauthorized();
        var salle = await _getSalleById.ExecuteAsync(id);

        if (salle == null) return NotFound("Salle non trouvée.");

        var dto = new SalleDto().ToDto(salle);
        return Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<SalleDto>>> GetToutesLesSalles()
    {
        GetToutesLesSalles _getToutesLesSalles = new GetToutesLesSalles(repositoryFactory);
        
        /*string role = "";
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

        if (!_getToutesLesSalles.IsAuthorized(role)) return Unauthorized();*/
        var salles = await _getToutesLesSalles.ExecuteAsync();
        var dtos = salles.Select(s => new SalleDto().ToDto(s)).ToList();

        return Ok(dtos);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> ModifierSalle(long id, [FromBody] SalleDto salleDto)
    {
        GetSalleById _getSalleById = new GetSalleById(repositoryFactory);
        ModifierInfoSalle _modifierInfoSalle = new ModifierInfoSalle(repositoryFactory);
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

        if (!_modifierInfoSalle.IsAuthorized(role)) return Unauthorized();
        var salle = await _getSalleById.ExecuteAsync(id);
        if (salle == null) return NotFound("Salle non trouvée.");

        await _modifierInfoSalle.ExecuteAsync(
            salle,
            salleDto.numero_salle,
            salleDto.capacite,
            salleDto.disponibilite_salle,
            salleDto.type
        );

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SupprimerSalle(long id)
    {
        GetSalleById _getSalleById = new GetSalleById(repositoryFactory);
        SupprimerUneSalle _supprimerUneSalle = new SupprimerUneSalle(repositoryFactory);
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

        if (!_supprimerUneSalle.IsAuthorized(role)) return Unauthorized();
        var salle = await _getSalleById.ExecuteAsync(id);
        if (salle == null) return NotFound("Salle non trouvée.");

        await _supprimerUneSalle.ExecuteAsync(salle);
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
