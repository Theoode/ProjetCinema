using ScrynDomain.Entities;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.FilmUseCases.Get;

public class GetTousLesFilms(IRepositoryFactory repositoryFactory)
{
    /**
     * Permet de récupérer tous les films du catalogue, utile pour afficher une liste de film dans le front.
     */
    public async Task<List<Film>?> ExecuteAsync()
    {
        await CheckBusinessRules();
        List<Film>? films = await repositoryFactory.FilmRepository().FindAllAsync();
        return films;
    }

    private async Task CheckBusinessRules()
    {
        ArgumentNullException.ThrowIfNull(repositoryFactory);
        IFilmRepository filmRepository=repositoryFactory.FilmRepository();
        ArgumentNullException.ThrowIfNull(filmRepository);
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur) || role.Equals(Roles.Cineaste) || role.Equals(Roles.Employe);
    }
}