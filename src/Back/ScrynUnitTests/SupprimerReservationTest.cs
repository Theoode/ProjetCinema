using Moq;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.FilmUseCases.Create;
using ScrynDomain.UseCases.FilmUseCases.Delete;
using ScrynDomain.UseCases.ReservationUseCases.Create;

namespace ScrynUnitTests;

public class SupprimerReservationTest
{
    public void Setup()
    {
    }

    [Test]
    public void Test()
    {
        Reservation reservationInitial = new Reservation
        {
           date_reservation = DateTime.MinValue
        };

        var mock = new Mock<IRepositoryFactory>(); //Mock de la repo
        var fauxReservationRepo = mock.Object;

        AjouterUneReservation reservationUseCase = new AjouterUneReservation(fauxReservationRepo); //faux repo pour lancer la classe UseCase
        var filmRepo =  reservationUseCase.ExecuteAsync(reservationInitial);
        SupprimerReservation ResSupp = new SupprimerReservation(fauxReservationRepo);
        var ResSupprimer = ResSupp.ExecuteAsync(reservationInitial);
        Assert.Equals(fauxReservationRepo.FilmRepository().FindAsync(reservationInitial),null);
    }
}