using Api.Helpers.Exceptions.CustomExceptions;
using System.Net;
using System.Text.Json;

namespace Api.Helpers.Exceptions
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionHandlerMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception exception)
            {
                var response = httpContext.Response;
                response.ContentType = "application/json";

                switch (exception)
                {
                    case NoContentException:
                        response.StatusCode = (int)HttpStatusCode.NoContent;
                        break;

                    case BadHttpRequestException:
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case UnauthorizedAccessException:
                        response.StatusCode = (int)HttpStatusCode.Unauthorized;
                        break;

                    case ForbiddenException:
                        response.StatusCode = (int)HttpStatusCode.Forbidden;
                        break;

                    case KeyNotFoundException:
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;

                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new {
                    //message = response.StatusCode != (int)HttpStatusCode.InternalServerError ? exception?.Message : "Erro interno."
                    message = exception?.Message
                });

                if (!response.StatusCode.Equals((int)HttpStatusCode.NoContent))
                {
                    await response.WriteAsync(result);
                }
            }
        }
    }
}
