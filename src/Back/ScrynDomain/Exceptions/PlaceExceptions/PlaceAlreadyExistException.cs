namespace ScrynDomain.Exceptions.PlaceExceptions;

public class PlaceAlreadyExistException : Exception
{
    public PlaceAlreadyExistException()
    {
    }
    public PlaceAlreadyExistException(string message)
    {
    }
    public PlaceAlreadyExistException(string message, Exception inner) : base(message, inner)
    {
    }
}