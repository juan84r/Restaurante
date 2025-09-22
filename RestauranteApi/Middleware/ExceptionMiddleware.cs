using System.Net;
using System.Text.Json;
using Aplication.Exceptions;

namespace RestauranteApi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
           catch (DuplicateEntityException dex)
            {
            _logger.LogWarning(dex, "Conflict: {Message}", dex.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = dex.Message }));
            }
            catch (NotFoundException nfe)
            {
            _logger.LogWarning(nfe, "NotFound: {Message}", nfe.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = nfe.Message }));
            }
            catch (BadRequestException bre)
            {
            _logger.LogWarning(bre, "BadRequest: {Message}", bre.Message);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsync(JsonSerializer.Serialize(new { message = bre.Message }));
            }
            catch (Exception ex)
            {
            _logger.LogError(ex, "Ocurrió una excepción no controlada");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var response = new { message = "Ocurrió un error interno en el servidor" };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            }
        }
    }
}
