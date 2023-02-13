namespace VanKassa.Presentation.BlazorWeb.Features.Shared.Exceptions;

public class QueryException : Exception
{
    public QueryException()
    {}

    public QueryException(string message) : base(message)
    {}

    public QueryException(string message, Exception inner) : base(message, inner)
    {}
}