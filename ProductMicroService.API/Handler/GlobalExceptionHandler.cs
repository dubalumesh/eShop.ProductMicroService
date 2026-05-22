using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ProductMicroService.API.Handler
{
    /// <summary>
    /// Global exception Handler which can execute as part of UseException Middleware 
    /// </summary>
    /// <param name="_logger">log the errors</param>
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> _logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError($"{exception.GetType().ToString()}: {exception.Message}");

            if (exception.InnerException != null)
            {

                _logger.LogError($"{exception.InnerException.GetType().ToString()}: {exception.InnerException.Message}");
            }
            // Use ProblemDetails for a standardized API response
            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Server Error",
                Detail = exception.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
