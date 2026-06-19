namespace ScrynDomain.Exceptions.PlaceExceptions;

public class PlaceDoesntExistException : Exception
{
    public PlaceDoesntExistException()
    {
    }
    public PlaceDoesntExistException(string message)
    {
    }
    public PlaceDoesntExistException(string message, Exception inner) : base(message, inner)
    {
    }
}