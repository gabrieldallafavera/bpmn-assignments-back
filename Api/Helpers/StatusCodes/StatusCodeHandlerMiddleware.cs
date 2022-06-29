using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace Api.Helpers.StatusCodes
{
    public class StatusCodeHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public StatusCodeHandlerMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            await _requestDelegate(context);

            var response = context.Response;

            if (!response.Headers.IsReadOnly)
            {
                if (response.StatusCode == (int)System.Net.HttpStatusCode.Unauthorized)
                {
                    response.ContentType = Application.Json;
                    await response.WriteAsync(JsonSerializer.Serialize("Não autenticado."));
                }
                else if (response.StatusCode == (int)System.Net.HttpStatusCode.Forbidden)
                {
                    response.ContentType = Application.Json;
                    await response.WriteAsync(JsonSerializer.Serialize("Acesso negado."));
                }
            }
        }
    }
}
