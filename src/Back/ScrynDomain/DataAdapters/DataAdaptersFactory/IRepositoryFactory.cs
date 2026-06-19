using System.Threading.Tasks.Dataflow;
using ScrynDomain.DataAdapters;

namespace ScrynDomain.DataAdapters.DataAdaptersFactory;

public interface IRepositoryFactory
{
    IFilmRepository FilmRepository();
    IFilmRepository CreateFilmRepository();
    IPlaceRepository PlaceRepository();
    IReservationRepository ReservationRepository();
    ITarifRepository TarifRepository();
    ISalleRepository SalleRepository();
    
    IScrynRoleRepository ScrynRole();

    IPaiementRepository PaiementRepository();
    
    IScrynUserRepository ScrynUser();
    
    ISeanceRepository SeanceRepository();
    ISeanceRepository CreateSeanceRepository();
    // Méthodes de gestion de la datasource
    // Ce sont des méthodes qui permettent de gérer l'ensemble du data source
    // comme par exemple tout supprimer ou tout créer
    Task EnsureDeletedAsync();
    Task EnsureCreatedAsync();
    Task SaveChangesAsync();
}