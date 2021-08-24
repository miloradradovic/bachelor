using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Model.models;

namespace BusinessLogicLayer.services
{
    
    public interface IMailService
    {
        public void SendEmail(MailRequest mailRequest);
    }
    
    public class MailService : IMailService
    {
        private readonly EmailSenderData _emailSenderData;

        public MailService(IOptions<EmailSenderData> emailSenderData)
        {
            _emailSenderData = emailSenderData.Value;
        }

        public async void SendEmail(MailRequest mailRequest)
        {
            mailRequest.Body = mailRequest.Body + "<br>Srdacno,<br>Administrator tim";
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(_emailSenderData.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(_emailSenderData.Host, _emailSenderData.Port, SecureSocketOptions.SslOnConnect);
            smtp.Authenticate(_emailSenderData.Mail, _emailSenderData.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
}