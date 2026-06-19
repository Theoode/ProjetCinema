using System.Linq.Expressions;
using ScrynDomain.Entities;
 
namespace ScrynDomain.DataAdapters;
 
public interface IFilmRepository : IRepository<Film>
{
    Task<Film?> FindFilmComplet(long id_film);
    Task AddAsync(Film film);
}