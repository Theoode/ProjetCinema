
using Moq;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.PlaceUseCases.Create;


public class ChoisirPlaceTest
{
    private readonly Mock<IRepositoryFactory> _repositoryFactoryMock;
    private readonly Mock<IPlaceRepository> _placeRepositoryMock;
    private readonly ChoisirPlace _choisirPlace;
    
    
    public ChoisirPlaceTest()
    {
        _repositoryFactoryMock = new Mock<IRepositoryFactory>();
        _placeRepositoryMock = new Mock<IPlaceRepository>();
        
        _repositoryFactoryMock.Setup(r => r.PlaceRepository())
            .Returns(_placeRepositoryMock.Object);

        _choisirPlace = new ChoisirPlace(_repositoryFactoryMock.Object);
    }
    
    [Test]
    public async Task ExecuteAsync_Should_Mark_Place_As_Unavailable()
    {
        // Arrange
        var placeId = 1;
        var place = new Place
            
        {
            id_place = placeId, 
            disponibilite = true
        };
        
        _placeRepositoryMock.Setup(r => r.FindAsync(placeId))
            .ReturnsAsync(place);
        _placeRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Place>()))
            .Returns(Task.CompletedTask);
        _repositoryFactoryMock.Setup(r => r.SaveChangesAsync())
            .Returns(Task.CompletedTask);

        // Act
        var result = await _choisirPlace.ExecuteAsync(placeId);

        // Assert
        Assert.NotNull(result);
        Assert.False(result.disponibilite);
        _placeRepositoryMock.Verify(r => r.UpdateAsync(place), Times.Once);
        _repositoryFactoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }
    
    [Test]
    public async Task ExecuteAsync_Should_Throw_Exception_When_Place_Not_Found()
    {
        // Arrange
        var placeId = 1;
        _placeRepositoryMock.Setup(r => r.FindAsync(placeId))
            .ReturnsAsync((Place)null);

        // Act & Assert
        Assert.ThrowsAsync<ArgumentNullException>(() => _choisirPlace.ExecuteAsync(placeId));
    }
}
