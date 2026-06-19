using ScrynDomain.Entities;

namespace ScrynDomain.Dtos;

public class SeanceDto
{
    public long id_seance { get; set; }
    public DateTime date_seance { get; set; }

    // ManyToMany : Une séance peut contenir plusieurs tarifs
    public List<TarifDto>? AppliqueSur { get; set; } = new();

    // ManyToMany : Une séance peut contenir plusieurs réservations
    public List<ReservationDto>? ContenuDans { get; set; } = new();

    // Film "léger"
    public FilmLightDto? Film { get; set; }
    public long fk_film { get; set; }

    // Salle "légère"
    public SalleLightDto? Salle { get; set; }
    public long fk_salle { get; set; }

    public SeanceDto ToDto(Seance seance)
    {
        id_seance = seance.id_seance;
        date_seance = seance.date_seance;
        AppliqueSur = TarifDto.ToDtos(seance.AppliqueSur);
        ContenuDans = ReservationDto.ToDtos(seance.ContenuDans);
        fk_film = seance.fk_film;
        fk_salle = seance.fk_salle;
        Salle = seance.Salle != null ? SalleLightDto.FromEntity(seance.Salle) : null;
        Film = seance.Film != null ? FilmLightDto.FromEntity(seance.Film) : null;
        return this;
    }

    public static List<SeanceDto> ToDtos(List<Seance> seances)
    {
        return seances.Select(seance => new SeanceDto().ToDto(seance)).ToList();
    }

    public static List<Seance> ToEntities(List<SeanceDto> dtos)
    {
        return dtos.Select(dto => dto.ToEntity()).ToList();
    }

    public Seance ToEntity()
    {
        return new Seance
        {
            id_seance = this.id_seance,
            date_seance = this.date_seance,
            AppliqueSur = TarifDto.ToEntities(this.AppliqueSur),
            ContenuDans = ReservationDto.ToEntities(this.ContenuDans),
            fk_film = this.fk_film,
            fk_salle = this.fk_salle,
            Salle = this.Salle?.ToEntity(),
            Film = this.Film?.ToEntity()
        };
    }
}
