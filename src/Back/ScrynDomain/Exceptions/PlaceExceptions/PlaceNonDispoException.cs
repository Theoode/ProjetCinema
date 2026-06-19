namespace ScrynDomain.Exceptions.PlaceExceptions;

public class PlaceNonDispoException : Exception
{
    
    public PlaceNonDispoException()
    {
    }
    public PlaceNonDispoException(string message)
    {
    }
    public PlaceNonDispoException(string message, Exception inner) : base(message, inner)
    {
    }
}