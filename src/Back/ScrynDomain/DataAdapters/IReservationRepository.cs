using System.Linq.Expressions;
using ScrynDomain.Entities;

namespace ScrynDomain.DataAdapters;

public interface IReservationRepository: IRepository<Reservation>
{
  public Task<Reservation?> FindReservationComplet(long id_reservation);

}