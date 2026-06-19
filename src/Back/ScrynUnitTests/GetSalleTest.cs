using Moq;
using System.Threading.Tasks;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.FilmUseCases.Get;
using ScrynDomain.UseCases.SalleUseCases.Get;

public class GetSalleCompletByIdTests
{
    [Test]
    public async Task ExecuteAsync_ShouldReturnSalle_WhenSalleExists()
    {
        // Arrange
        var mockSalleRepository = new Mock<ISalleRepository>();
        var mockRepositoryFactory = new Mock<IRepositoryFactory>();
        
        var salle = new Salle { id_salle = 1};
        mockSalleRepository.Setup(repo => repo.FindAsync(1)).ReturnsAsync(salle);
        mockRepositoryFactory.Setup(factory => factory.SalleRepository()).Returns(mockSalleRepository.Object);
        
        var useCase = new GetSalleCompletById(mockRepositoryFactory.Object);
        
        // Act
        var result = await useCase.ExecuteAsync(1);
        
        // Assert
        Assert.NotNull(result);
        Assert.Equals(1, result.id_salle);
    }

    [Test]
    public async Task ExecuteAsync_ShouldReturnNull_WhenSalleDoesNotExist()
    {
        // Arrange
        var mockSalleRepository = new Mock<ISalleRepository>();
        var mockRepositoryFactory = new Mock<IRepositoryFactory>();
        
        mockSalleRepository.Setup(repo => repo.FindAsync(1)).ReturnsAsync((Salle?)null);
        mockRepositoryFactory.Setup(factory => factory.SalleRepository()).Returns(mockSalleRepository.Object);
        
        var useCaseSalleCompletById = new GetSalleCompletById(mockRepositoryFactory.Object);
        var useCaseSalleById = new GetSalleById(mockRepositoryFactory.Object);
        var useCaseSalles = new GetToutesLesSalles(mockRepositoryFactory.Object);
        // Act
        var resultSalleComplet = await useCaseSalleCompletById.ExecuteAsync(1);
        var resultSalleById = await useCaseSalleById.ExecuteAsync(1);
        var resultSalles= await useCaseSalles.ExecuteAsync();
        
        // Assert
        Assert.NotNull(resultSalleComplet);
        Assert.NotNull(resultSalleById);
        Assert.NotNull(resultSalles);
    }
}