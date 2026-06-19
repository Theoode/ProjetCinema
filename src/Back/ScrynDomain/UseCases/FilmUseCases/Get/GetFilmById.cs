using ScrynDomain.Entities;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.FilmUseCases.Get;

public class GetFilmById(IRepositoryFactory repositoryFactory)
{
    /**
     * Permet la récupération d'un film selon un identifiant de film.
     */
    public async Task<Film?> ExecuteAsync(long idFilm)
    {
        await CheckBusinessRules();
        Film? film = await repositoryFactory.FilmRepository().FindFilmComplet(idFilm);
        return film;
    }


    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        IFilmRepository filmRepository = repositoryFactory.FilmRepository();
        ArgumentNullException.ThrowIfNull(filmRepository);
    }
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}