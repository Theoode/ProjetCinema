using Moq;
using NUnit.Framework;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.Exceptions.ReservationExceptions;
using ScrynDomain.UseCases.ReservationUseCases.Get;
using System;
using System.Threading.Tasks;
using ScrynDomain.DataAdapters;

namespace ScrynUnitTests;

[TestFixture]
public class GetReservation
{
    private Mock<IRepositoryFactory> _repositoryFactoryMock;
    private Mock<IReservationRepository> _reservationRepositoryMock;
    private VoirReservation _voirReservation;

    [SetUp]
    public void Setup()
    {
        _repositoryFactoryMock = new Mock<IRepositoryFactory>();
        _reservationRepositoryMock = new Mock<IReservationRepository>();
        _repositoryFactoryMock.Setup(r => r.ReservationRepository()).Returns(_reservationRepositoryMock.Object);
        _voirReservation = new VoirReservation(_repositoryFactoryMock.Object);
    }

    [Test]
    public async Task ExecuteAsync_ShouldReturnReservation_WhenReservationExists()
    {
        // Arrange
        long reservationId = 1;
        var expectedReservation = new Reservation { id_reservation = reservationId, date_reservation = new DateTime(2015, 12, 25) };
        _reservationRepositoryMock.Setup(repo => repo.FindAsync(reservationId)).ReturnsAsync(expectedReservation);
        _reservationRepositoryMock.Setup(repo => repo.FindReservationComplet(reservationId)).ReturnsAsync(expectedReservation);

        // Act
        var result = await _voirReservation.ExecuteAsync(reservationId,"mail");

        // Assert
        Assert.IsNotNull(result);
        Assert.AreEqual(expectedReservation.id_reservation, result.id_reservation);
        Assert.AreEqual(expectedReservation.date_reservation, result.date_reservation);
    }

    [Test]
    public void ExecuteAsync_ShouldThrowException_WhenReservationDoesNotExist()
    {
        // Arrange
        long reservationId = 2;
        _reservationRepositoryMock.Setup(repo => repo.FindAsync(reservationId)).ReturnsAsync((Reservation?)null);

        // Act & Assert
        var ex = Assert.ThrowsAsync<ReservationDoesntExistException>(async () => await _voirReservation.ExecuteAsync(reservationId,"mail"));
        Assert.AreEqual("Aucune réservation trouvée avec cet identifiant.", ex.Message);
    }
}