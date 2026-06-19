using Moq;
using ScrynDomain.Entities;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.UseCases.SeanceUseCase.Get;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScrynUnitTests
{
    public class GetToutesLesSeancesTest
    {
        [Test]
        public async Task Test_GetToutesLesSeances_ReturnsCorrectSeances()
        {
            var seances = new List<Seance>
            {
                new Seance
                {
                    id_seance = 1,
                    Film = new Film { id_film = 1, nom_film = "Minecraft" },
                    Salle = new Salle { id_salle = 1, numero_salle = 1 },
                    date_seance = DateTime.Now
                },
                new Seance
                {
                    id_seance = 2,
                    Film = new Film { id_film = 2, nom_film = "Fortnite" },
                    Salle = new Salle { id_salle = 2, numero_salle = 2 },
                    date_seance = DateTime.Now.AddHours(1)
                }
            };

            var mockRepositoryFactory = new Mock<IRepositoryFactory>();
            var mockSeanceRepository = new Mock<ISeanceRepository>();

            mockSeanceRepository.Setup(repo => repo.FindAllAsync())
                .ReturnsAsync(seances);

            mockRepositoryFactory.Setup(factory => factory.SeanceRepository())
                .Returns(mockSeanceRepository.Object);

            var getToutesLesSeances = new GetToutesLesSeances(mockRepositoryFactory.Object);

            List<Seance>? result = await getToutesLesSeances.ExecuteAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result?.Count);
            Assert.AreEqual(seances[0].id_seance, result?[0].id_seance);
            Assert.AreEqual(seances[1].Film.nom_film, result?[1].Film.nom_film);

            mockSeanceRepository.Verify(repo => repo.FindAllAsync(), Times.Once);
        }
    }
}