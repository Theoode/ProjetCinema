using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;
using ScrynDomain.UseCases.FilmUseCases.Get;

namespace ScrynDomain.UseCases.SeanceUseCase.Create;

public class AjouterUneSeance (IRepositoryFactory repositoryFactory)
{
    public async Task<Seance> ExecuteAsync(DateTime date_seance, List<Tarif>? AppliqueSur, List<Reservation>? ContenuDans, long fk_film, long fk_salle )
    {
        var film = repositoryFactory.FilmRepository().FindAsync(fk_film);
        var salle = repositoryFactory.SalleRepository().FindAsync(fk_salle);
            
        var seance = new Seance
        {
            date_seance = date_seance,
            AppliqueSur = AppliqueSur,
            ContenuDans = ContenuDans,
            Film = film.Result,
            Salle = salle.Result
        };
        return await ExecuteAsync(seance);
    }
    public async Task<Seance> ExecuteAsync(Seance seance)
    {
        CheckBusinessRules(seance);
        Seance seanceFin = await repositoryFactory.SeanceRepository().CreateAsync(seance);
        repositoryFactory.FilmRepository().SaveChangesAsync().Wait();
        return seance;
    }

    private async Task CheckBusinessRules(Seance seance)
    {
        ArgumentNullException.ThrowIfNull(seance);
        ArgumentNullException.ThrowIfNull(repositoryFactory.SeanceRepository());
        ArgumentNullException.ThrowIfNull(seance.date_seance);
        ArgumentNullException.ThrowIfNull(seance.Film);
        ArgumentNullException.ThrowIfNull(seance.Salle);
    }
    
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) ||  role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
    //Ecrire fonction de rôle ici.
    
}