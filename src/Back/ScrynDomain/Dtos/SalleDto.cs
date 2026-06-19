using ScrynDomain.Entities;

namespace ScrynDomain.Dtos;

public class SalleDto
{
    public long id_salle { get; set; }
    
    public long numero_salle { get; set; }
    
    public long capacite { get; set; }
    
    public bool disponibilite_salle { get; set; }
    
    public string type { get; set; }
    
    public List<PlaceDto>? PresenteDans { get; set; } = new();
    
    public List<SeanceDto>? ContenuDans { get; set; } = new();

    public SalleDto ToDto(Salle salle)
    {
        id_salle=salle.id_salle;
        numero_salle=salle.numero_salle;
        capacite=salle.capacite;
        disponibilite_salle=salle.disponibilite_salle;
        type=salle.type;
        if(salle.PresenteDans!=null) PresenteDans = PlaceDto.ToDtos(salle.PresenteDans);
        if (salle.ContenuDans!=null) ContenuDans = SeanceDto.ToDtos(salle.ContenuDans);
        return this;
    }
    
    public Salle ToEntity()
    {
        return new Salle
        {
           id_salle = this.id_salle,
           numero_salle = this.numero_salle,
           capacite = this.capacite,
           disponibilite_salle = this.disponibilite_salle,
           type=this.type,
           PresenteDans = PlaceDto.ToEntities(this.PresenteDans),
           ContenuDans = SeanceDto.ToEntities(this.ContenuDans)
        };
    }
}


