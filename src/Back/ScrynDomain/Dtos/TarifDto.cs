using ScrynDomain.Entities;

namespace ScrynDomain.Dtos;

public class TarifDto
{
    public long id_tarif { get; set; }
    public string nom_tarif { get; set; }
    public float valeur { get; set; }
    public DateTime date_deb { get; set; }
    public DateTime date_fin { get; set; }

    public TarifDto ToDto(Tarif tarif)
    {
        return new TarifDto
        {
            id_tarif = tarif.id_tarif,
            nom_tarif = tarif.nom_tarif,
            valeur = tarif.valeur,
            date_deb = tarif.date_deb,
            date_fin = tarif.date_fin
        };
    }

    public static List<TarifDto> ToDtos(List<Tarif>? tarifs)
    {
        return tarifs?.Select(t => new TarifDto().ToDto(t)).ToList() ?? new();
    }

    public Tarif ToEntity()
    {
        return new Tarif
        {
            id_tarif = this.id_tarif,
            nom_tarif = this.nom_tarif,
            valeur = this.valeur,
            date_deb = this.date_deb,
            date_fin = this.date_fin
        };
    }

    public static List<Tarif> ToEntities(List<TarifDto>? dtos)
    {
        return dtos?.Select(dto => dto.ToEntity()).ToList() ?? new();
    }
}
