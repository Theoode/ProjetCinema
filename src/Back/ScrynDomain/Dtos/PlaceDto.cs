using ScrynDomain.Entities;

namespace ScrynDomain.Dtos;

public class PlaceDto
{
    public long id_place { get; set; }
    
    public long numero_place { get; set; }
    
    public bool disponibilite { get; set; }
    
    public Salle FaitPartie { get; set; } = new();
    public long fk_salle { get; set; }
    
    public Reservation Reservation { get; set; } = new();
    
    public long fk_reservation { get; set; }


    public PlaceDto ToDto(Place place)
    {
        id_place = place.id_place;
        numero_place = place.numero_place;
        disponibilite = place.disponibilite;
        fk_salle = place.fk_salle;
        fk_reservation = place.fk_reservation;
        Reservation = place.Reservation;
        FaitPartie = place.FaitPartie;
        return this;
    }
    

    public static List<PlaceDto> ToDtos(List<Place> places)
    {
        List<PlaceDto> dtos = new List<PlaceDto>();
        foreach (var place in places)
        {
            dtos.Add(new PlaceDto().ToDto(place));   
        }
        return dtos;
    }
    
    public static List<Place> ToEntities(List<PlaceDto> places)
    {
        List<Place> dtos = new List<Place>();
        foreach (var place in places)
        {
            dtos.Add(place.ToEntity());
        }
        return dtos;
    }
    
    public Place ToEntity()
    {
        return new Place
        {
            
            id_place = this.id_place, numero_place = this.numero_place, disponibilite = this.disponibilite,
            fk_salle = this.fk_salle, fk_reservation = this.fk_reservation,
            FaitPartie = this.FaitPartie , Reservation = this.Reservation
        };
    }
}