using Microsoft.AspNetCore.Diagnostics;
using ResultPattern.Exceptions;
using System.Net;

namespace ResultPatternSample.Infrastructure
{
    public class GlobalErrorHandeling : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            Type type = exception.GetType();

            return type switch
            {
                var ex when ex == typeof(AppException)
                => await HandleException(httpContext, exception, HttpStatusCode.BadRequest),

                var ex when ex == typeof(NotFoundException)
                => await HandleException(httpContext, exception, HttpStatusCode.NotFound),

                _ => await HandleException(httpContext, exception, HttpStatusCode.InternalServerError)
            };
        }

        private async Task<bool> HandleException(HttpContext httpContext, Exception exception, HttpStatusCode code)
        {
            httpContext.Response.StatusCode = (int)code;
            var response = BaseResult.Fail(exception.Message);
            if (httpContext.Response.StatusCode == 500)
            {
                response.ErrorMessage = "unhandeled error";
            }
            await httpContext.Response.WriteAsJsonAsync(response);
            return true;
        }
    }
}
