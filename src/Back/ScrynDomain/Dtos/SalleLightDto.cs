using ScrynDomain.Entities;

namespace ScrynDomain.Dtos;

public class SalleLightDto
{
    public long id_salle { get; set; }
    public long numero_salle { get; set; }

    public static SalleLightDto FromEntity(Salle salle)
    {
        return new SalleLightDto
        {
            id_salle = salle.id_salle,
            numero_salle = salle.numero_salle
        };
    }

    public Salle ToEntity()
    {
        return new Salle
        {
            id_salle = this.id_salle,
            numero_salle = this.numero_salle
        };
    }
}
