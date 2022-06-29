using Microsoft.AspNetCore.Diagnostics;

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

            if (response.StatusCode == (int)System.Net.HttpStatusCode.Unauthorized)
            {
                response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
                await response.WriteAsync(System.Text.Json.JsonSerializer.Serialize("Não autenticado."));
            }
            else if (response.StatusCode == (int)System.Net.HttpStatusCode.Forbidden)
            {
                response.ContentType = System.Net.Mime.MediaTypeNames.Application.Json;
                await response.WriteAsync(System.Text.Json.JsonSerializer.Serialize("Acesso negado."));
            }
        }
    }
}
