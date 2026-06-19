namespace ScrynDomain.Entities;

public class Reservation
{
    public long id_reservation { get; set; }
    
    public DateTime date_reservation { get; set; }
    
    //ManyToMany : Une réservation peut contenir plusieurs places
    public List<Place>? ContientDans { get; set; } = new();
    
    public Seance Seance { get; set; } = new();
    public long fk_seance { get; set; }
    
    public Paiement fk_paiement { get; set; }
    
    public IUtilisateur Utilisateur { get; set; }
}