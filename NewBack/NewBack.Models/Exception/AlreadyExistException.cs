namespace NewBack.Models.Exception;

public class AlreadyExistException: System.Exception
{
    public AlreadyExistException() {}

    public AlreadyExistException(string message) : base(message) {}
    
    public AlreadyExistException(string message, System.Exception inner) : base(message, inner) {}

}