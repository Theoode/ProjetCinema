namespace ScrynDomain.Exceptions.SalleExceptions;

public class TarifAlreadyExistException : Exception
{
    public TarifAlreadyExistException()
    {
    }

    public TarifAlreadyExistException(string message) : base(message)
    {
    }

    public TarifAlreadyExistException(string message, Exception inner) : base(message, inner)
    {
    }
}