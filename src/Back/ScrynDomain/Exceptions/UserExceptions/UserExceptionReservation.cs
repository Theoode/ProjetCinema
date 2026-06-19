namespace ScrynDomain.Exceptions;

public class UserExceptionReservation : Exception
{
    public UserExceptionReservation(){}
    public UserExceptionReservation(string message) : base(message){}
    public UserExceptionReservation(string message, Exception inner) : base(message, inner){}
    
    
}