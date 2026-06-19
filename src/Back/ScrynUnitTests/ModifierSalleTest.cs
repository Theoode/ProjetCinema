using Moq;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.SalleUseCases.Create;

public class ModifierSalleTest
{


    [Test]
    public async Task Test1()
    {
        Salle salleInitiale = new Salle
        {
            numero_salle = 1,
            capacite = 14,
            disponibilite_salle = true,
            type = "Normal"
        };

        var mock = new Mock<IRepositoryFactory>(); // Mock du repository
        var fauxSalleRepo = mock.Object;

        AjouterUneSalle salleAjoutUseCase = new AjouterUneSalle(fauxSalleRepo);
        await salleAjoutUseCase.ExecuteAsync(1, 14, true, "Normal");

        ModifierInfoSalle salleUseCase = new ModifierInfoSalle(fauxSalleRepo);
        await salleUseCase.ExecuteAsync(salleInitiale, 2, 144, false, "pas normal");

        var salleRepo = await fauxSalleRepo.SalleRepository()
            .FindByConditionAsync(salle => salle.id_salle == salleInitiale.id_salle);

        Assert.AreEqual(false, salleRepo.First().disponibilite_salle); // Vérifie que la dispo a bien changé
    }
}