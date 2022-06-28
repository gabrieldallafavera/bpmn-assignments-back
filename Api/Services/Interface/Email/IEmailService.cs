using Api.Models.Email;

namespace Api.Services.Interface.Email
{
    public interface IEmailService
    {
        void SendEmail(EmailRequest emailRequest);
    }
}
