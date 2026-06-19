using ScrynDomain.Dtos;

namespace ScrynDomain.DataAdapters;

public interface ICreateSeanceUseCase
{
    Task ExecuteAsync(CreateSeanceDto dto);
}