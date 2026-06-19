using System.Threading.Tasks.Dataflow;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Esf;
using Scryn.Repositories;
using ScrynDataProvider.Entities;
using ScrynDataProvider.Repositories;
using ScrynDomain.DataAdapters;
using ScrynDomain.DataAdapters.DataAdaptersFactory;
using WebApplication1.Data;
using WebApplication1.Repositories;
using InvalidOperationException = System.InvalidOperationException;

namespace Scryn.RepositoryFactory;

public class RepositoryFactory(ScrynDbContext context,UserManager<ScrynUser> userManager,RoleManager<ScrynRole> roleManager): IRepositoryFactory
{
    private IFilmRepository? _filmRepository;
    private IPlaceRepository? _placeRepository;
    private IReservationRepository? _reservationRepository;
    private IScrynRoleRepository? _scrynRoleRepository;
    private IScrynUserRepository? _scrynUserRepository;
    private ITarifRepository? _tarifRepository;
    private ISalleRepository? _salleRepository;
    private ISeanceRepository? _seanceRepository;
    private IPaiementRepository? _paiementRepository;
    public IFilmRepository FilmRepository()
    {
        if (_filmRepository == null)
        {
            _filmRepository = new FilmRepository(context ?? throw new InvalidOperationException());
        }

        return _filmRepository;
    }  
    public IPaiementRepository PaiementRepository()
    {
        if (_paiementRepository == null)
        {
            _paiementRepository = new PaiementRepository(context ?? throw new InvalidOperationException());
        }

        return _paiementRepository;
    }  
    
    public IFilmRepository CreateFilmRepository()
    {
        return new FilmRepository(context ?? throw new InvalidOperationException());
    }
    
    public ISeanceRepository SeanceRepository()
    {
        if (_seanceRepository == null)
        {
            _seanceRepository = new SeanceRepository(context ?? throw new InvalidOperationException());
        }

        return _seanceRepository;
    }

    public ISeanceRepository CreateSeanceRepository()
    {
        return new SeanceRepository(context ?? throw new InvalidOperationException());
    }

    public IPlaceRepository PlaceRepository()
    {
        if (_placeRepository == null)
        {
            _placeRepository = new  PlaceRepository(context ?? throw new InvalidOperationException());
        }

        return _placeRepository;
    }  
    public ITarifRepository TarifRepository()
    {
        if (_tarifRepository == null)
        {
            _tarifRepository = new TarifRepository(context ?? throw new InvalidOperationException());
        }

        return _tarifRepository;
    }  
    public IReservationRepository ReservationRepository()
    {
        if (_reservationRepository == null)
        {
            _reservationRepository = new ReservationRepository(context ?? throw new InvalidOperationException());
        }

        return _reservationRepository;
    } 
    public ISalleRepository SalleRepository()
    {
        if (_salleRepository == null)
        {
            _salleRepository = new SalleRepository(context ?? throw new InvalidOperationException());
        }

        return _salleRepository;
    }
    public IScrynUserRepository ScrynUser()
    {
        if (_scrynUserRepository == null)
        {
            _scrynUserRepository = new ScrynUserRepository(context ?? throw new InvalidOperationException(),userManager, roleManager);
        }

        return _scrynUserRepository;
    }
    
    public IScrynRoleRepository ScrynRole()
    {
        if (_scrynRoleRepository == null)
        {
            _scrynRoleRepository = new ScrynRoleRepository(context ?? throw new InvalidOperationException(),roleManager);
        }

        return _scrynRoleRepository;
    }
    
    public async Task SaveChangesAsync()
    {
        context.SaveChangesAsync().Wait();
    }
    public async Task EnsureCreatedAsync()
    {
        context.Database.EnsureCreated();
    }
    public async Task EnsureDeletedAsync()
    {
        context.Database.EnsureDeleted();
    }
}