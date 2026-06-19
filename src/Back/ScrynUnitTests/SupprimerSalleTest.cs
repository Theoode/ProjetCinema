using Microsoft.EntityFrameworkCore.Diagnostics;
using Moq;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.FilmUseCases.Create;
using ScrynDomain.UseCases.SalleUseCases.Create;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.UseCases.SalleUseCases.Delete;

namespace ScrynUnitTests;

public class SupprimerUnTarifTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test()
    {
        Salle salleInitiale = new Salle
        {
            numero_salle = 1,
            capacite = 14,
            disponibilite_salle = true,
            type = "Normal"
        };

        var mock = new Mock<IRepositoryFactory>(); //Mock de la repo
        var fauxSalleRepo = mock.Object;

        AjouterUneSalle salle = new AjouterUneSalle(fauxSalleRepo); //faux repo pour lancer la classe UseCase
        
        var salleTest = salle.ExecuteAsync(1, 14, true, "Normal");
        
        SupprimerUneSalle supprimerUneSalle = new SupprimerUneSalle(fauxSalleRepo);
        var salleSupp = supprimerUneSalle.ExecuteAsync(salleInitiale);
        Assert.Equals(fauxSalleRepo.SalleRepository().FindAsync(salleInitiale), null);
    }
}