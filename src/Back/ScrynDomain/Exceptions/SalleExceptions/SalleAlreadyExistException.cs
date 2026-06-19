namespace ScrynDomain.Exceptions.SalleExceptions;

public class SalleAlreadyExistException : Exception
{
    public SalleAlreadyExistException()
    {
    }

    public SalleAlreadyExistException(string message) : base(message)
    {
    }

    public SalleAlreadyExistException(string message, Exception inner) : base(message, inner)
    {
    }
}