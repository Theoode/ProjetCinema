using ScrynDomain.DataAdapters.DataAdaptersFactory;
using ScrynDomain.Entities;

namespace ScrynDomain.UseCases.SeanceUseCase.Update;

public class ModifierInfoSeance(IRepositoryFactory repositoryFactory) {


    public async Task ExecuteAsync(Seance seance, DateTime date_seance, List<Tarif>? AppliqueSur, List<Reservation>? ContenuDans, Film film, Salle salle){
        
        var seanceUpdate = await repositoryFactory.SeanceRepository().FindAsync(seance.id_seance);
        await CheckBusinessRules(seanceUpdate);
        seanceUpdate.date_seance = date_seance;
        seanceUpdate.AppliqueSur = AppliqueSur;
        seanceUpdate.ContenuDans = ContenuDans;
        seanceUpdate.Film = film;
        seanceUpdate.Salle = salle;
        
        

        
       await repositoryFactory.SeanceRepository().UpdateAsync(seanceUpdate);
       await repositoryFactory.SeanceRepository().SaveChangesAsync();
    }

    private async Task CheckBusinessRules(Seance seance){
        ArgumentNullException.ThrowIfNull(seance, nameof(seance));
        ArgumentNullException.ThrowIfNull(repositoryFactory.SeanceRepository());
        //suite à réfléchir
    }
    public bool IsAuthorized(string role)
    {
        return role.Equals(Roles.Employe) || role.Equals(Roles.DirecteurTechnique) || role.Equals(Roles.Directeur);
    }
}