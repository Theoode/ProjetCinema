using ScrynDomain.DataAdapters;
using ScrynDomain.Dtos;
using ScrynDomain.Entities;

namespace ScrynApplication.UseCases.FilmUseCases.Create
{
    public class CreateFilmUseCase : ICreateFilmUseCase
    {
        private readonly IFilmRepository _filmRepo;

        public CreateFilmUseCase(IFilmRepository filmRepo)
        {
            _filmRepo = filmRepo;
        }

        public async Task ExecuteAsync(CreateFilmDto dto)
        {
            var film = new Film
            {
                nom_film = dto.nom_film,
                auteur = dto.auteur,
                description = dto.description,
                duree = dto.duree,
                date_sortie = dto.date_sortie,
                affiche = dto.affiche,
                FaitPartie = new List<Genre>(),
                Seances = new List<Seance>()
            };

            await _filmRepo.AddAsync(film);
        }
    }
}