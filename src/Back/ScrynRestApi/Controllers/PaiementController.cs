using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Dtos;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.PaiementUseCases.Create;
using ScrynDomain.UseCases.PaiementUseCases.Get;
using ScrynDomain.UseCases.PlaceUseCases.Delete;
using ScrynDomain.UseCases.PlaceUseCases.Get;
using ScrynDomain.UseCases.UserUseCases;

namespace ScrynRestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaiementController(IRepositoryFactory repositoryFactory) : ControllerBase
{
    [HttpPost("{id}")]
    public async Task<ActionResult<PaiementDto>> AjouterPaiement([FromBody] PaiementDto dto)
    {
        AjouterPaiement _ajouterReservation = new AjouterPaiement(repositoryFactory);
        string role = "";
        string email = "";
        IUtilisateur user = null;
        var paiement = dto.ToEntity();
        try
        {
            CheckSecu(out role, out email, out user);
        }
        catch
        {
            return Unauthorized();
        }
        
        var paiementRe = await _ajouterReservation.ExecuteAsync(paiement);
        var dtoRe = new PaiementDto().ToDto(paiementRe);
        return Ok(dtoRe);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PaiementDto>> GetPaiement(long id)
    {
        GetPaiementById _getPaiementById = new GetPaiementById(repositoryFactory);

        var paiement = await _getPaiementById.ExecuteAsync(id);
        if (paiement == null) return NotFound("Place non trouvée.");

        PaiementDto dto = new PaiementDto().ToDto(paiement);
        return Ok(dto);
    }

    [HttpGet("complet/{id}")]
    public async Task<ActionResult<PaiementDto>> GetPaiementComplet(long id)
    {
        GetPaiementCompletById _getPaiementCompletById = new GetPaiementCompletById(repositoryFactory);
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

        if (!_getPaiementCompletById.IsAuthorized(role)) return Unauthorized();

        var paiement = await _getPaiementCompletById.ExecuteAsync(id);
        if (paiement == null) return NotFound("Place complète non trouvée.");

        var dto = new PaiementDto().ToDto(paiement);
        return Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PlaceDto>>> GetAllPaiements()
    {
        GetTousLesPaiements _getTousLesPaiements = new GetTousLesPaiements(repositoryFactory);
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

        if (!_getTousLesPaiements.IsAuthorized(role)) return Unauthorized();

        var paiements = await _getTousLesPaiements.ExecuteAsync();
        var dtos = paiements.Select(p => new PaiementDto().ToDto(p)).ToList();

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