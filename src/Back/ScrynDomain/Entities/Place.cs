namespace ScrynDomain.Entities;

public class Place
{
    public long id_place { get; set; }
    
    public long numero_place { get; set; }
    
    public bool disponibilite { get; set; }
    
    public Salle FaitPartie { get; set; } = new();
    public long fk_salle { get; set; }
    
    //ManyToMany : La place est comprise dans une ou plusieurs réservation 
    public Reservation Reservation { get; set; } = new();
    
    public long fk_reservation { get; set; }
}