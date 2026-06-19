using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Dtos;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.FilmUseCases.Delete;
using ScrynDomain.UseCases.ReservationUseCases.Create;
using ScrynDomain.UseCases.ReservationUseCases.Get;
using ScrynDomain.UseCases.RoleUseCases.Get;
using ScrynDomain.UseCases.UserUseCases;

namespace ScrynRestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController(IRepositoryFactory repositoryFactory) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<ReservationDto>> AjouterReservation([FromBody] ReservationDto reservationDto)
    {
        if (reservationDto == null) return BadRequest("Les informations de la réservation sont requises.");
        Reservation reservation = reservationDto.ToEntity();
        AjouterUneReservation _ajouterReservation = new AjouterUneReservation(repositoryFactory);
        string role = "";
        string email = "";
        IUtilisateur user = null;

        /*try
        {
            CheckSecu(out role, out email, out user);
        }
        catch
        {
            return Unauthorized();
        }*/

        //if (!_ajouterReservation.IsAuthorized(role)) return Unauthorized();
        await _ajouterReservation.ExecuteAsync(reservation);
        ReservationDto dto = new ReservationDto().ToDto(reservation);
        return CreatedAtAction(nameof(GetReservation), new { id = dto?.id_reservation }, dto);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ReservationDto>> GetReservation(long id)
    {
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

        VoirReservation _getReservationById = new VoirReservation(repositoryFactory);
        var reservation = await _getReservationById.ExecuteAsync(id,email);
        if (reservation == null) return NotFound("Réservation non trouvée.");

        ReservationDto dto = new ReservationDto().ToDto(reservation);
        return Ok(dto);
    }
    

    [HttpDelete("{id}")]
    public async Task<IActionResult> SupprimerReservation(long id)
    {
        VoirReservation _getReservationById = new VoirReservation(repositoryFactory);
        SupprimerReservation _supprimerReservation = new SupprimerReservation(repositoryFactory);
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

        if (!_getReservationById.IsAuthorized(role) || !_supprimerReservation.IsAuthorized(role)) return Unauthorized();

        var reservation = await _getReservationById.ExecuteAsync(id,email);
        if (reservation == null) return NotFound("Réservation non trouvée.");

        await _supprimerReservation.ExecuteAsync(reservation);
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
