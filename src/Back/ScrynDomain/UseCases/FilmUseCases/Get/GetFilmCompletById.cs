using ScrynDomain.Entities;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.FilmUseCases.Get;

public class GetFilmCompletById(IRepositoryFactory repositoryFactory)
{
/**
 * Permet la récupération d'un film avec tous les éléménts liés a celui ci, les séances etc....
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
        ArgumentNullException.ThrowIfNull(repositoryFactory.FilmRepository());
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.Cineaste) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
    //Ecrire IsAuthorized plus tard.
}
