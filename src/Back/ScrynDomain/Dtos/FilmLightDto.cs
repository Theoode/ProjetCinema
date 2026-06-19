using ScrynDomain.Entities;

public class FilmLightDto
{
    public long id_film { get; set; }
    public string nom_film { get; set; }

    public static FilmLightDto FromEntity(Film film)
    {
        return new FilmLightDto
        {
            id_film = film.id_film,
            nom_film = film.nom_film
        };
    }

    public Film ToEntity()
    {
        return new Film
        {
            id_film = this.id_film,
            nom_film = this.nom_film
        };
    }
}
