using ScrynDomain.Entities;

namespace ScrynDomain.Dtos;

public class ReservationDto
{
    public long id_reservation { get; set; }
    public DateTime date_reservation { get; set; }

    public List<PlaceDto>? ContientDans { get; set; } = new();

    public long fk_seance { get; set; }

    public long? fk_paiement { get; set; }

    public ReservationDto ToDto(Reservation reservation)
    {
        id_reservation = reservation.id_reservation;
        date_reservation = reservation.date_reservation;
        fk_seance = reservation.fk_seance;

        fk_paiement = reservation.fk_paiement?.id_paiement;

        if (reservation.ContientDans != null)
            ContientDans = PlaceDto.ToDtos(reservation.ContientDans);

        return this;
    }

    public static List<ReservationDto> ToDtos(List<Reservation> reservations)
    {
        return reservations.Select(r => new ReservationDto().ToDto(r)).ToList();
    }

    public static List<Reservation> ToEntities(List<ReservationDto> reservations)
    {
        return reservations.Select(r => r.ToEntity()).ToList();
    }

    public Reservation ToEntity()
    {
        return new Reservation
        {
            id_reservation = this.id_reservation,
            date_reservation = this.date_reservation,
            ContientDans = PlaceDto.ToEntities(this.ContientDans),
            fk_seance = this.fk_seance,

            fk_paiement = this.fk_paiement != null
                ? new Paiement { id_paiement = this.fk_paiement.Value }
                : null
        };
    }
}
