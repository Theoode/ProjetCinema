namespace ScrynDomain.Exceptions.SalleExceptions;

public class TarifDoesntExistException : Exception
{
    public TarifDoesntExistException()
    {
    }

    public TarifDoesntExistException(string message) : base(message)
    {
    }

    public TarifDoesntExistException(string message, Exception inner) : base(message, inner)
    {
    }
}