namespace ScrynDomain.Exceptions.FilmExceptions;

public class FilmDoesntExistException : Exception
{
    public FilmDoesntExistException()
    {
    }

    public FilmDoesntExistException(string message) : base(message)
    {
    }

    public FilmDoesntExistException(string message, Exception inner) : base(message, inner)
    {
    }
}