using Moq;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.SeanceUseCase.Delete;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ScrynDomain.DataAdapters;
using NUnit.Framework;

namespace ScrynUnitTests
{
    public class SupprimerSeanceTest
    {
        public void Setup()
        {
        }

        [Test]
        public async Task Test()
        {
            Seance seanceInitiale = new Seance
            {
                id_seance = 1,
                date_seance = DateTime.Now,
                Film = new Film { id_film = 1, nom_film = "Minecraft" },
                Salle = new Salle { id_salle = 1, numero_salle = 1 }
            };

            var mock = new Mock<IRepositoryFactory>();
            var fauxSeanceRepo = new Mock<ISeanceRepository>();

            fauxSeanceRepo.Setup(x => x.FindByConditionAsync(It.IsAny<Expression<Func<Seance, bool>>>()))
                .ReturnsAsync(new List<Seance> { seanceInitiale });

            fauxSeanceRepo.Setup(x => x.DeleteAsync(It.IsAny<Seance>())).Returns(Task.CompletedTask);

            mock.Setup(x => x.SeanceRepository()).Returns(fauxSeanceRepo.Object);

            // On créé l'instance du useCase SupprimerSeance
            var supprimerSeance = new SupprimerSeance(mock.Object);
            
            await supprimerSeance.ExecuteAsync(seanceInitiale); //Ici on supprime la séance

            // Et on vérifie que la fonction à été appelée
            fauxSeanceRepo.Verify(x => x.DeleteAsync(It.Is<Seance>(s => s.id_seance == seanceInitiale.id_seance)), Times.Once);
        }
    }
}