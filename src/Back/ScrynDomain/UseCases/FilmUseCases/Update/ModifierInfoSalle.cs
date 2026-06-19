using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;

public class ModifierInfoFilm(IRepositoryFactory repositoryFactory) {

/**
 * Modification des informations d'un Film.
 */
    public async Task ExecuteAsync(Film film, string nom_film, string auteur, string description, string duree, DateTime date_sortie, string affiche, List<Genre>? faitPartie){
        
        var filmUpdate = await repositoryFactory.FilmRepository().FindAsync(film.id_film);
        await CheckBusinessRules(filmUpdate);
        filmUpdate.nom_film = nom_film;
        filmUpdate.auteur = auteur;
        filmUpdate.description = description;
        filmUpdate.duree = duree;
        filmUpdate.date_sortie = date_sortie;
        filmUpdate.affiche = affiche;
        filmUpdate.FaitPartie = faitPartie;

        
       await repositoryFactory.FilmRepository().UpdateAsync(filmUpdate);
       await repositoryFactory.FilmRepository().SaveChangesAsync();
    }

    private async Task CheckBusinessRules(Film film){
        ArgumentNullException.ThrowIfNull(film, nameof(film));
        ArgumentNullException.ThrowIfNull(repositoryFactory.FilmRepository());
        //suite à réfléchir
    }

}