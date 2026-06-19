using Moq;
using ScrynDomain.Entities;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.UseCases.SeanceUseCase.Update;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ScrynUnitTests
{
    public class ModifierInfoSeanceTest
    {
        [Test]
        public async Task Test_ModifierInfoSeance_UpdatesSeance()
        {
            var seance = new Seance
            {
                id_seance = 1,
                date_seance = DateTime.Now,
                Film = new Film { id_film = 1, nom_film = "Minecraft" },
                Salle = new Salle { id_salle = 1, numero_salle = 1 }
            };

            var newFilm = new Film { id_film = 2, nom_film = "Fortnite" };
            var newSalle = new Salle { id_salle = 2, numero_salle = 2 };
            DateTime newDateSeance = DateTime.Now.AddDays(1);
            var newTarifs = new List<Tarif>();
            var newReservations = new List<Reservation>();

            var mockRepositoryFactory = new Mock<IRepositoryFactory>();
            var mockSeanceRepository = new Mock<ISeanceRepository>();

            mockSeanceRepository.Setup(repo => repo.FindAsync(seance.id_seance))
                .ReturnsAsync(seance);

            mockSeanceRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Seance>()))
                .Returns(Task.CompletedTask);

            mockSeanceRepository.Setup(repo => repo.SaveChangesAsync())
                .Returns(Task.CompletedTask);

            mockRepositoryFactory.Setup(factory => factory.SeanceRepository())
                .Returns(mockSeanceRepository.Object);

            var modifierInfoSeance = new ModifierInfoSeance(mockRepositoryFactory.Object);

            await modifierInfoSeance.ExecuteAsync(seance, newDateSeance, newTarifs, newReservations, newFilm, newSalle);

            Assert.AreEqual(newDateSeance, seance.date_seance);
            Assert.AreEqual(newFilm.id_film, seance.Film.id_film);
            Assert.AreEqual(newSalle.id_salle, seance.Salle.id_salle);

            mockSeanceRepository.Verify(repo => repo.UpdateAsync(It.Is<Seance>(s =>
                s.id_seance == seance.id_seance &&
                s.date_seance == newDateSeance &&
                s.Film.id_film == newFilm.id_film &&
                s.Salle.id_salle == newSalle.id_salle)), Times.Once);

            mockSeanceRepository.Verify(repo => repo.SaveChangesAsync(), Times.Once);
        }
    }
}