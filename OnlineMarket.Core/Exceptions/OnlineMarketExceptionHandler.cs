using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace OnlineMarket.Core.Exceptions
{
    public class OnlineMarketExceptionHandler(ILogger<OnlineMarketExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var statusCode = exception switch
            {
                OnlineMarketException onlineMarketException => onlineMarketException.StatusCode,
                _ => StatusCodes.Status500InternalServerError
            };

            if (statusCode >= StatusCodes.Status500InternalServerError)
                logger.LogError(exception, "Exception occurred: {Message}", exception.Message);
            else
                logger.LogWarning(exception, "Request failed: {Message}", exception.Message);

            httpContext.Response.StatusCode = statusCode;

            var error = new
            {
                Id = Guid.NewGuid(),
                StatusCode = statusCode,
                ErrorMessage = exception.Message
            };

            await httpContext.Response.WriteAsJsonAsync(error, cancellationToken);

            return true;
        }
    }
}
