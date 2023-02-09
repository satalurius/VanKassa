namespace VanKassa.Domain.Exceptions;

/// <summary>
/// Кастомное исключение, выбрасываемое если какие-то из запрашиваемых данных не были найдены.
/// </summary>
public class NotFoundException : Exception
{
    public NotFoundException()
    {
    }
    
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string message, Exception exception) : base(message, exception)
    {
    }
}