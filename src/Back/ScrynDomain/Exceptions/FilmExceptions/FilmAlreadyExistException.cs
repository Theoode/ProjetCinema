namespace ScrynDomain.Exceptions.FilmExceptions;

public class FilmAlreadyExistException : Exception
{
    public FilmAlreadyExistException()
    {
    }

    public FilmAlreadyExistException(string message) : base(message)
    {
    }

    public FilmAlreadyExistException(string message, Exception inner) : base(message, inner)
    {
    }
}