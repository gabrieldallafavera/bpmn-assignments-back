using Api.Models.Email;
using Api.Services.Interface.Email;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Api.Services.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmail(EmailRequest emailRequest)
        {
            var email = new MimeMessage();
            
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("Email:From").Value));
            email.To.Add(MailboxAddress.Parse(emailRequest.To));
            if (!string.IsNullOrEmpty(emailRequest.Cc))
            {
                email.Cc.Add(MailboxAddress.Parse(emailRequest.Cc));
            }
            email.Subject = emailRequest.Subject;

            var builder = new BodyBuilder();

            builder.TextBody = emailRequest.Body;

            if (emailRequest.Attachments != null)
            {
                foreach (var item in emailRequest.Attachments)
                {
                    builder.Attachments.Add(item.FileName, new MemoryStream(Convert.FromBase64String(item.FileBase64)));
                }
            }

            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_configuration.GetSection("Email:Host").Value, int.Parse(_configuration.GetSection("Email:Port").Value), SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_configuration.GetSection("Email:Username").Value, _configuration.GetSection("Email:Password").Value);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
