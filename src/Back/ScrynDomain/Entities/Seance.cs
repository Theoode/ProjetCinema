using ScrynDomain.UseCases.FilmUseCases.Get;

namespace ScrynDomain.Entities;

public class Seance
{
    public long id_seance { get; set; }
    
    public DateTime date_seance { get; set; }
    
    //ManyToMany : Une séance peut contenir plusieurs tarifs
    public List<Tarif>? AppliqueSur { get; set; } = new();
    
    //ManyToMany : Une séance peut contenir plusieurs reservation
    public List<Reservation>? ContenuDans { get; set; } = new();
    
    //OneToMany : Une séance contient un film

    public Film? Film { get; set; }
    public long fk_film { get; set; }
    
    //OneToMany : Une séance contient un film
    public Salle Salle { get; set; }
    public long fk_salle { get; set; }
}