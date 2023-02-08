namespace VanKassa.Domain.Exceptions;

public class ExceptionResponse
{
    public string Message { get; private set; }
    
    public ExceptionResponse(string message)
    {
        Message = message;
    }
}