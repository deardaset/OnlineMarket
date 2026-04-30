using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineMarket.Core.Exceptions
{
    public class OnlineMarketExceptionHandler(ILogger<OnlineMarketExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "Exception occured: {Message}", exception.Message);

            var statuscode = exception switch
            {
                OnlineMarketException x => x.StatusCode,
                _ => StatusCodes.Status500InternalServerError
            };
            httpContext.Response.StatusCode = statuscode;

            var error = new
            {
                Id = Guid.NewGuid(),
                StatusCode = statuscode,
                ErrorMessage = exception.Message
            };

            await httpContext.Response.WriteAsJsonAsync(error, cancellationToken);

            return true;
        }
    }
}
