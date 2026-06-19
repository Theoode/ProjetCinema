using ScrynDomain.DataAdapters;
using ScrynDomain.Dtos;
using ScrynDomain.Entities;

namespace ScrynDomain.UseCases.SeanceUseCase.Create;

public class CreateSeanceUseCase : ICreateSeanceUseCase
{
    private readonly ISeanceRepository _seanceRepo;

    public CreateSeanceUseCase(ISeanceRepository seanceRepo)
    {
        _seanceRepo = seanceRepo;
    }

    public async Task ExecuteAsync(CreateSeanceDto dto)
    {
        var seance = new Seance
        {
            date_seance = dto.date_seance,
            fk_film = dto.fk_film,
            fk_salle = dto.fk_salle
        };

        await _seanceRepo.AddAsync(seance);
    }
}