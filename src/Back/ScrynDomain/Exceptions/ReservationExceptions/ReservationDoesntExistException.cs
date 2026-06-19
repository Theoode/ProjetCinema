namespace ScrynDomain.Exceptions.ReservationExceptions;

public class ReservationDoesntExistException : Exception
{
    public ReservationDoesntExistException()
    {
    }

    public ReservationDoesntExistException(string message) : base(message)
    {
    }

    public ReservationDoesntExistException(string message, Exception inner) : base(message, inner)
    {
    }
}