using Api.Models.Email;
using Api.Services.Interface.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("email")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// Envia email
        /// </summary>
        /// <remarks>
        /// Exemplo de request:
        /// 
        ///     Post /EmailRequest
        ///     {
        ///         "to": "exemplo@email.com",
        ///         "cc": "exemplocc@email.com", /* Opcional */
        ///         "subject": "Exemplo de assunto",
        ///         "body": "Corpo do email.",
        ///         "attachments": [
        ///             {
        ///                 "fileName": "arquivo.pdf",
        ///                 "fileBase64": "f7383n49fmf3mf9iu39..." /* arquivo formato base64 */
        ///             },
        ///             {
        ///                 "fileName": "arquivo.jpg",
        ///                 "fileBase64": "f7383n49fhd823hdf24..." /* arquivo formato base64 */
        ///             }
        ///         ]
        ///     }
        /// </remarks>
        /// <param name="emailRequest">Objeto EmailRequest</param>
        /// <response code="204">Email enviado</response>
        [HttpPost]
        public async Task<IActionResult> SendEmail(EmailRequest emailRequest)
        {
            await _emailService.SendEmail(emailRequest);

            return NoContent();
        }
    }
}
