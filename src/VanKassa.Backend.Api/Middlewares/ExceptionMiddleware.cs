using System.Net;
using VanKassa.Domain.Exceptions;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace VanKassa.Backend.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception ex)
        {
            var error = new ExceptionResponse(ex.Message);
            
            httpContext.Response.Clear();

            httpContext.Response.ContentType = "application/json";

            httpContext.Response.StatusCode = GetStatusCodeByException(ex);

            await httpContext.Response.WriteAsync(JsonSerializer.Serialize<ExceptionResponse>(error));
        }
    }

    private int GetStatusCodeByException(Exception ex)
        => ex switch
        {
            BadRequestException => (int)HttpStatusCode.BadRequest,
            ForbiddenException => (int)HttpStatusCode.Forbidden,
            NotFoundException => (int)HttpStatusCode.NotFound,
            UnauthorizedException => (int)HttpStatusCode.Unauthorized,
            _ => (int)HttpStatusCode.InternalServerError
        };
}