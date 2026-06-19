namespace ScrynDomain.Entities;

public class Film
{
    public long id_film { get; set; }
    
    public string nom_film { get; set; }
    
    public string auteur { get; set; }
    
    public string description { get; set; }
    
    public string duree { get; set; }
    
    public DateTime date_sortie { get; set; }
    
    public string affiche { get; set; }
    
    //ManyToMany : Le film appartient à un ou plusieurs genres
    public List<Genre>? FaitPartie { get; set; } = new();

    public List<Seance>? Seances { get; set; } = null;
}