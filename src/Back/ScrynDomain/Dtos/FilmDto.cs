using ScrynDomain.Entities;
using ScrynDomain.Dtos;

public class FilmDto
{
    public long id_film { get; set; }
    public string nom_film { get; set; }
    public string auteur { get; set; }
    public string description { get; set; }
    public string duree { get; set; }
    public DateTime date_sortie { get; set; }
    public string affiche { get; set; }

    public List<GenreDto>? FaitPartie { get; set; } = new();
    public List<SeanceDto>? Seances { get; set; } = null;

    public FilmDto ToDto(Film film)
    {
        id_film = film.id_film;
        nom_film = film.nom_film;
        auteur = film.auteur;
        description = film.description;
        duree = film.duree;
        date_sortie = film.date_sortie;
        affiche = film.affiche;

        if (film.FaitPartie != null)
            FaitPartie = GenreDto.ToDtosWithoutFilms(film.FaitPartie);

        if (film.Seances != null)
            Seances = SeanceDto.ToDtos(film.Seances);

        return this;
    }

    public static List<FilmDto> ToDtos(List<Film> films)
    {
        return films.Select(f => new FilmDto().ToDto(f)).ToList();
    }

    public Film ToEntity()
    {
        return new Film
        {
            id_film = this.id_film,
            nom_film = this.nom_film,
            auteur = this.auteur,
            description = this.description,
            duree = this.duree,
            date_sortie = this.date_sortie,
            affiche = this.affiche,
            FaitPartie = FaitPartie != null ? GenreDto.ToEntities(FaitPartie) : new List<Genre>(),
            Seances = Seances != null ? SeanceDto.ToEntities(Seances) : new List<Seance>()
        };
    }
}
