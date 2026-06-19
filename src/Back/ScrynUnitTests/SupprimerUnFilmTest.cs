using Moq;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.FilmUseCases.Create;
using ScrynDomain.UseCases.FilmUseCases.Delete;

namespace ScrynUnitTests;

public class SupprimerUnFilmTest 
{
    
    public void Setup()
    {
    }

    [Test]
    public void Test()
    {
        var listGenre = new List<Genre>();
        listGenre.Add(new Genre{id_genre = 1,nom_genre = "Drame"});
        
        Film filmInitial = new Film
        {
            duree = "1 heure et 25 min",
            auteur = "Martin Scorsese",
            description = "Film super, parlant de la démolition des cultures des natifs américains",
            FaitPartie = listGenre,
            nom_film = "Killers of the flowermoon",
            date_sortie = new DateTime(2023, 10, 20),
            affiche = " "
        };

        var mock = new Mock<IRepositoryFactory>(); //Mock de la repo
        var fauxFilmRepo = mock.Object;

        AjouterUnFilm filmUseCase = new AjouterUnFilm(fauxFilmRepo); //faux repo pour lancer la classe UseCase
        var filmRepo =  filmUseCase.ExecuteAsync("1 heure et 25 min",
            "Martin Scorsese",
            "Film super, parlant de la démolition des cultures des natifs américains",
            "Killers of the flowermoon",
            new DateTime(2023, 10, 20),
            " ",
            listGenre);
        SupprimerFilm filmSup = new SupprimerFilm(fauxFilmRepo);
        var filmSupp = filmSup.ExecuteAsync(filmInitial);
        Assert.Equals(fauxFilmRepo.FilmRepository().FindAsync(filmInitial),null);
    }
}