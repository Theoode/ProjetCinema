namespace ScrynDomain.Exceptions.ReservationExceptions;

public class ReservationAlreadyExist: Exception
{
    public ReservationAlreadyExist()
    {
    }
    public ReservationAlreadyExist(string message)
    {
    }
    public ReservationAlreadyExist(string message, Exception inner) : base(message, inner)
    {
    }
}