using Api.Models.Email;

namespace Api.Services.Interface.Email
{
    public interface IEmailService
    {
        Task SendEmail(EmailRequest emailRequest);
    }
}
