using System.Text.Json;
using Backend.Exceptions;
using Backend.Modules.Users.Application.Exceptions;

namespace Backend.Middlewares;

public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger
    )
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            await WriteProblem(context, 400, ex.Code, ex.Message);
        }
        catch (InvalidCredentialsException ex)
        {
            await WriteProblem(context, 401, ex.Code, ex.Message);
        }
        catch (AppException ex)
        {
            await WriteProblem(context, 409, ex.Code, ex.Message);
        }
        catch (InfrastructureException ex)
        {
            _logger.LogError(ex, "Infrastructure error");

            await WriteProblem(
                context,
                503,
                ex.Code,
                ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            await WriteProblem(context, 500, "server.error", "An unexpected error occurred.");
        }
    }

    private static async Task WriteProblem(
        HttpContext context,
        int status,
        string code,
        string message
    )
    {
        context.Response.StatusCode = status;
        context.Response.ContentType = "application/json";

        var body = new
        {
            code,
            message,
            status
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(body));
    }
}
