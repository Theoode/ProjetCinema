using ScrynDomain.Entities;

namespace ScrynDomain.Dtos;

public class GenreDto
{
    public long id_genre { get; set; }
    public string nom_genre { get; set; }

    public GenreDto ToDto(Genre genre)
    {
        id_genre = genre.id_genre;
        nom_genre = genre.nom_genre;
        return this;
    }

    public static List<Genre> ToEntities(List<GenreDto>? dtos)
    {
        if (dtos == null) return new List<Genre>();
        
        return dtos.Select(dto => new Genre
        {
            id_genre = dto.id_genre,
            nom_genre = dto.nom_genre
        }).ToList();
    }

    public static List<GenreDto> ToDtos(List<Genre> genres)
    {
        return genres.Select(g => new GenreDto().ToDto(g)).ToList();
    }

    public static List<GenreDto> ToDtosWithoutFilms(List<Genre> genres)
    {
        return genres.Select(g => new GenreDto
        {
            id_genre = g.id_genre,
            nom_genre = g.nom_genre
        }).ToList();
    }
}
