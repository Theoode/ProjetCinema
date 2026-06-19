namespace ScrynDomain.Exceptions.SalleExceptions;

public class SalleDoesntExistException: Exception
{
    public SalleDoesntExistException()
    {
    }

    public SalleDoesntExistException(string message) : base(message)
    {
    }

    public SalleDoesntExistException(string message, Exception inner) : base(message, inner)
    {
    }
}