namespace ScrynDomain.Dtos;

public class CreateSeanceDto
{
    public DateTime date_seance { get; set; }
    public long fk_film { get; set; }
    public long fk_salle { get; set; }
}