using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.Exceptions.FilmExceptions;
using ScrynDomain.DataAdapters.DataAdaptersFactory;

namespace ScrynDomain.UseCases.FilmUseCases.Create;

public class AjouterUnFilm (IRepositoryFactory repositoryFactory)
{
    public async Task<Film> ExecuteAsync(string nom_film, string auteur, string description, string duree, DateTime dateSortie, string affiche, List<Genre> genre)
    {
        var film = new Film
        {
            nom_film = nom_film, auteur = auteur, description = description, affiche = affiche,
            date_sortie = dateSortie, FaitPartie = genre, duree = duree
        };
        return await ExecuteAsync(film);
    }
    public async Task<Film> ExecuteAsync(Film film)
    {
        CheckBusinessRules(film);
        Film filmRe = await repositoryFactory.FilmRepository().CreateAsync(film);
        repositoryFactory.FilmRepository().SaveChangesAsync().Wait();
        return filmRe;
    }

    private async Task CheckBusinessRules(Film film)
    {
        ArgumentNullException.ThrowIfNull(film);
        ArgumentNullException.ThrowIfNull(repositoryFactory.FilmRepository()); //Vérifications de la repository
        ArgumentNullException.ThrowIfNull(film.nom_film); //Nom du film doit être renseigné
        ArgumentNullException.ThrowIfNull(film.date_sortie); //Date de sortie doit être renseignée
        
        List<Film>? films = await repositoryFactory
                .FilmRepository()
                .FindByConditionAsync(f => f.id_film.Equals(film.id_film));
        //trouve une liste de films avec l'identifiant du film passé en paramètre de la fonction.

        if(films.Any()) throw
        new FilmAlreadyExistException("Ce film existe déjà dans le catalogue");
    }
    
    
    public bool IsAuthorized(string role)
    {
        if (role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur)) return true; // Seul le Directeur et le Directeur Technique a accès a cette méthode.
        return false;
    }
    
}