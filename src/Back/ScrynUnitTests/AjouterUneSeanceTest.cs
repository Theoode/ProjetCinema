using Moq;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.SeanceUseCase.Create;

namespace ScrynUnitTests;

public class AjouterUneSeanceTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task AjouterUneSeance_Test()
    {
        var dateSeance = new DateTime(2025, 4, 2, 15, 0, 0);
        var tarifs = new List<Tarif>();
        var reservations = new List<Reservation>();
        var film = new Film { id_film = 1, nom_film = "Minecraft" };
        var salle = new Salle { id_salle = 1, numero_salle = 1 };

        var seanceInitiale = new Seance
        {
            date_seance = dateSeance,
            AppliqueSur = tarifs,
            ContenuDans = reservations,
            Film = film,
            Salle = salle
        };

        var mock = new Mock<IRepositoryFactory>();
        var mockSeanceRepo = new Mock<ISeanceRepository>();
        var mockFilmRepo = new Mock<IFilmRepository>();

        mockSeanceRepo.Setup(x => x.CreateAsync(It.IsAny<Seance>())).ReturnsAsync(seanceInitiale);
        mockFilmRepo.Setup(x => x.SaveChangesAsync()).Returns(Task.CompletedTask);

        mock.Setup(x => x.SeanceRepository()).Returns(mockSeanceRepo.Object);
        mock.Setup(x => x.FilmRepository()).Returns(mockFilmRepo.Object);

        var ajouterUneSeance = new AjouterUneSeance(mock.Object);
        var seanceAjoutee = await ajouterUneSeance.ExecuteAsync(dateSeance, tarifs, reservations, film, salle);

        Assert.NotNull(seanceAjoutee);
        Assert.AreEqual(dateSeance, seanceAjoutee.date_seance);
        
        //On vérifie que le film est bien associé
        Assert.AreEqual(film, seanceAjoutee.Film);
        
        // On vérifie que la salle correspond bien
        Assert.AreEqual(salle, seanceAjoutee.Salle);

        mockSeanceRepo.Verify(x => x.CreateAsync(It.IsAny<Seance>()), Times.Once);
        mockFilmRepo.Verify(x => x.SaveChangesAsync(), Times.Once);
    }
}