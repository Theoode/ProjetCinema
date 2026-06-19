using ScrynDomain.Dtos;

namespace ScrynApplication.UseCases.FilmUseCases.Create
{
    public interface ICreateFilmUseCase
    {
        Task ExecuteAsync(CreateFilmDto dto);
    }
}