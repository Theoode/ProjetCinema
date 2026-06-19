using System.Linq.Expressions;
using ScrynDomain.Entities;

namespace ScrynDomain.DataAdapters;

public interface IPlaceRepository: IRepository<Place>
{
    Task<Place?> FindPlaceComplet(long idPlace);
}