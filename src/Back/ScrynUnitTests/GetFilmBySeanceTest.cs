using Moq;
using ScrynDomain.Entities;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.UseCases.SeanceUseCase.Get;
using NUnit.Framework;
using System.Threading.Tasks;

namespace ScrynUnitTests
{
    public class GetFilmBySeanceTest
    {
        [Test]
        public async Task Test_GetFilmBySeance_ReturnsCorrectSeance()
        {
            long idSeance = 1;
            Seance seance = new Seance
            {
                id_seance = idSeance,
                Film = new Film { id_film = 1, nom_film = "Minecraft" },
                Salle = new Salle { id_salle = 1, numero_salle = 1 },
                date_seance = DateTime.Now
            };

            var mockRepositoryFactory = new Mock<IRepositoryFactory>();
            var mockSeanceRepository = new Mock<ISeanceRepository>();

            mockSeanceRepository.Setup(repo => repo.FindAsync(idSeance))
                .ReturnsAsync(seance);

            mockRepositoryFactory.Setup(factory => factory.SeanceRepository())
                .Returns(mockSeanceRepository.Object);

            var getFilmBySeance = new GetFilmBySeance(mockRepositoryFactory.Object);
            
            Seance? result = await getFilmBySeance.ExecuteAsync(idSeance);

            Assert.IsNotNull(result);
            Assert.AreEqual(seance.id_seance, result?.id_seance);
            Assert.AreEqual(seance.Film.nom_film, result?.Film.nom_film);

            mockSeanceRepository.Verify(repo => repo.FindAsync(idSeance), Times.Once);
        }
    }
}