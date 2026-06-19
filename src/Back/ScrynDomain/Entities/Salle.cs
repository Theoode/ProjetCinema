namespace ScrynDomain.Entities;

public class Salle
{
    public long id_salle { get; set; }
    
    public long numero_salle { get; set; }
    
    public long capacite { get; set; }
    
    public bool disponibilite_salle { get; set; }
    
    public string type { get; set; }
    
    //OneToMany : Une salle contient plusieurs places
    public List<Place>? PresenteDans { get; set; } = new();
    
    //OneToMany : Une salle peut contenir plusieurs seances
    public List<Seance>? ContenuDans { get; set; } = new();

}