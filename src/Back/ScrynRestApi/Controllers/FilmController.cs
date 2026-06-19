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
using ScrynDomain.UseCases.UserUseCases;

namespace ScrynRestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilmController(IRepositoryFactory repositoryFactory) : ControllerBase
{

    [HttpPost]
    public async Task<IActionResult> CreateFilm([FromBody] CreateFilmDto dto)
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

        var filmRepo = repositoryFactory.CreateFilmRepository();
        var createSeanceUseCase = new CreateFilmUseCase(filmRepo);

        await createSeanceUseCase.ExecuteAsync(dto);

        return Ok();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<FilmDto>> GetFilm(long id)
    {
        GetFilmById _getFilmById = new GetFilmById(repositoryFactory);

        var film = await _getFilmById.ExecuteAsync(id);
        if (film == null) return NotFound("Film non trouvé.");

        FilmDto dto = new FilmDto().ToDto(film);
        return Ok(dto);
    }


    [HttpGet("complet/{id}")]
    public async Task<ActionResult<FilmDto>> GetFilmComplet(long id)
    {
        GetFilmCompletById _getFilmCompletById = new GetFilmCompletById(repositoryFactory);
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
        var film = await _getFilmCompletById.ExecuteAsync(id);
        if (film == null) return NotFound("Film complet non trouvé.");
     
        FilmDto dto = new FilmDto().ToDto(film);
        return Ok(dto);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FilmDto>>> GetAllFilms()
    {
        GetTousLesFilms _getAllFilms = new GetTousLesFilms(repositoryFactory);
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
        var films = await _getAllFilms.ExecuteAsync();
        var dtos = films.Select(f => new FilmDto().ToDto(f)).ToList();

        return Ok(dtos);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> SupprimerFilm(long id)
    {
        GetFilmById _getFilmById = new GetFilmById(repositoryFactory);
        SupprimerFilm _supprimerFilm = new SupprimerFilm(repositoryFactory);
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

        //if (!_getFilmById.IsAuthorized(role) || !_supprimerFilm.IsAuthorized(role)) return Unauthorized();
        var film = await _getFilmById.ExecuteAsync(id);
        if (film == null) return NotFound("Film non trouvé.");

        await _supprimerFilm.ExecuteAsync(film);
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