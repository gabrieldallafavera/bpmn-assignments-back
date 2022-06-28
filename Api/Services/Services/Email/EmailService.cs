﻿using Api.Models.Email;
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
            //https://ethereal.email Usado para criar falsos smtp
            var email = new MimeMessage();
            
            email.From.Add(MailboxAddress.Parse(_configuration.GetSection("Email:From").Value));
            email.To.Add(MailboxAddress.Parse(emailRequest.To));
            if (!string.IsNullOrEmpty(emailRequest.Cc))
            {
                email.Cc.Add(MailboxAddress.Parse(emailRequest.Cc));
            }
            email.Subject = emailRequest.Subject;

            //Opção com bodyBuilder

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


            //Opção enviando o arquivo
            //var message = new TextPart(TextFormat.Html) { Text = body }; // TextFormat.RichText, TextFormat.Plain, TextFormat.Text, ...

            //var attachment = new MimePart("pdf")
            //{
            //    Content = new MimeContent(new Stream(new byte[32])), //new MimeContent(File.OpenRead({Caminho}), ContentEncoding.Default)
            //    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
            //    ContentTransferEncoding = ContentEncoding.Base64,
            //    FileName = Path.GetFileName("Nome.pdf")
            //};

            //var multipart = new Multipart("mixed");
            //multipart.Add(message);
            //multipart.Add(attachment);

            //email.Body = multipart;

            using var smtp = new SmtpClient(); // Usar Mailkit using ao invés do System
            await smtp.ConnectAsync(_configuration.GetSection("Email:Host").Value, int.Parse(_configuration.GetSection("Email:Port").Value), SecureSocketOptions.StartTls); // SecureSocketOptions.Auto, SecureSocketOptions.SslOnConnect, verificar possibilidades
            await smtp.AuthenticateAsync(_configuration.GetSection("Email:Username").Value, _configuration.GetSection("Email:Password").Value);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
