using System.ComponentModel.Design;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.Exceptions.SalleExceptions;

namespace ScrynDomain.UseCases.FilmUseCases.Delete;

public class SupprimerFilm(IRepositoryFactory repositoryFactory)
{

    public async Task ExecuteAsync(Film film)
    {
        await CheckBusinessRules(film);
        await repositoryFactory.FilmRepository().DeleteAsync(film); //Supprime le film grace a la fonction du repository
        await repositoryFactory.SaveChangesAsync();
    }

    private async Task CheckBusinessRules(Film film)
    {
        ArgumentNullException.ThrowIfNull(film);
        ArgumentNullException.ThrowIfNull(repositoryFactory.FilmRepository());

        var films = await repositoryFactory.FilmRepository()
            .FindByConditionAsync(f => f.id_film.Equals(film.id_film));

        if (!films.Any()) throw new SalleDoesntExistException();
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}