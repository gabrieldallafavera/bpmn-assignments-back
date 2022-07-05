using Api.Helpers.Exceptions.CustomExceptions;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

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
                response.ContentType = Application.Json;

                string result = string.Empty;

                if (exception.InnerException?.InnerException is SqlException sqlException)
                {
                    switch(sqlException.Number){
                        case 2601:  // Erro dado duplicado
                            response.StatusCode = (int)HttpStatusCode.BadRequest;
                            string duplicado = sqlException.Message.Split("(")[1].Split(")")[0];
                            result = $"O valor {duplicado} já existe.";
                            break;
                        default:
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            result = sqlException.Message;
                            break;
                    }

                    result = JsonSerializer.Serialize(new
                    {
                        message = result
                    });
                }
                else
                {
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

                        case KeyNotFoundException:
                            response.StatusCode = (int)HttpStatusCode.NotFound;
                            break;

                        default:
                            response.StatusCode = (int)HttpStatusCode.InternalServerError;
                            break;
                    }

                    result = JsonSerializer.Serialize(
#if !DEBUG
                        response.StatusCode != (int)HttpStatusCode.InternalServerError ? exception?.Message : "Erro interno."
#else
                        exception?.Message
#endif
                    );
                }
                
                if (!response.StatusCode.Equals((int)HttpStatusCode.NoContent))
                {
                    await response.WriteAsync(result);
                }
            }
        }
    }
}
