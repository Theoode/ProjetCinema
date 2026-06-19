namespace ScrynDomain.Dtos
{
    public class CreateFilmDto
    {
        public string nom_film { get; set; }
        public string auteur { get; set; }
        public string description { get; set; }
        public string duree { get; set; }
        public DateTime date_sortie { get; set; }
        public string affiche { get; set; }
    }
}