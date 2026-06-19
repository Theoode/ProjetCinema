namespace ScrynDomain.Entities;

public class Tarif
{
    public long id_tarif { get; set; }
    
    public string nom_tarif { get; set; }
    
    public float valeur { get; set; }
    
    public DateTime date_deb { get; set; }
    
    public DateTime date_fin { get; set; }
    
    //ManyToMany : Le tarif peut etre appliqué a une ou plusieurs séances 
    public List<Seance>? AppliqueDans { get; set; } = new();
}