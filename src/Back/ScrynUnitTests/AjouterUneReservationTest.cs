using System.Linq.Expressions;
using Moq;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.Exceptions.ReservationExceptions;
using ScrynDomain.UseCases.ReservationUseCases.Create;
using ScrynDomain.DataAdapters;

[TestFixture]
public class AjouterUneReservationTests
{
    private Mock<IRepositoryFactory> _repositoryFactoryMock;
    private Mock<IReservationRepository> _reservationRepositoryMock;
    private AjouterUneReservation _ajouterUneReservation;

    [SetUp]
    public void Setup()
    {
        _repositoryFactoryMock = new Mock<IRepositoryFactory>();
        _reservationRepositoryMock = new Mock<IReservationRepository>();
        _repositoryFactoryMock.Setup(r => r.ReservationRepository()).Returns(_reservationRepositoryMock.Object);
        _ajouterUneReservation = new AjouterUneReservation(_repositoryFactoryMock.Object);
    }

    [Test]
    public void ExecuteAsync_ShouldCreateReservation_WhenValid()
    {
        // Arrange
        var reservation = new Reservation { id_reservation = 1, date_reservation = DateTime.UtcNow };
        _reservationRepositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Reservation, bool>>>() ))
            .ReturnsAsync(new List<Reservation>());
        _reservationRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Reservation>()))
            .ReturnsAsync(reservation);

        // Act
        var result = _ajouterUneReservation.ExecuteAsync(reservation).Result;

        // Assert
        Assert.NotNull(result);
        Assert.AreEqual(reservation.id_reservation, result.id_reservation);
        _reservationRepositoryMock.Verify(r => r.SaveChangesAsync(), Times.Once);
    }

    [Test]
    public void ExecuteAsync_ShouldThrowException_WhenReservationExists()
    {
        // Arrange
        var reservation = new Reservation { id_reservation = 1, date_reservation = DateTime.UtcNow };
        _reservationRepositoryMock.Setup(r => r.FindByConditionAsync(It.IsAny<Expression<Func<Reservation, bool>>>() ))
            .ReturnsAsync(new List<Reservation> { reservation });

        // Act & Assert
        Assert.ThrowsAsync<ReservationAlreadyExist>(() => _ajouterUneReservation.ExecuteAsync(reservation));
    }

    [Test]
    public void ExecuteAsync_ShouldThrowArgumentNullException_WhenReservationIsNull()
    {
        // Act & Assert
        Assert.ThrowsAsync<ArgumentNullException>(() => _ajouterUneReservation.ExecuteAsync(null));
    }
}