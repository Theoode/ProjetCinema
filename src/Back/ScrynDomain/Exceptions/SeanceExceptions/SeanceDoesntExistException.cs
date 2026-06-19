namespace ScrynDomain.Exceptions.SeanceExceptions;

public class SeanceDoesntExistException : Exception
{
    public SeanceDoesntExistException()
    {
    }

    public SeanceDoesntExistException(string message) : base(message)
    {
    }

    public SeanceDoesntExistException(string message, Exception inner) : base(message, inner)
    {
    }
}