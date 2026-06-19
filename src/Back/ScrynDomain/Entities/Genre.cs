namespace ScrynDomain.Entities;

public class Genre
{
    public long id_genre { get; set; }
    
    public string nom_genre { get; set; }
    
    //ManyToMany : Le genre peut appartenir à un ou plusieurs films
    public List<Film>? Appartient { get; set; } = new();
    
}