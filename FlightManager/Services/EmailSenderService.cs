using FlightManager.Common;
using FlightManager.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace FlightManager.Services
{
    public class EmailSenderService : IEmailSenderService
    {
        private readonly IOptions<MailSettings> mailSettings;

        public EmailSenderService(IOptions<MailSettings> mailSettings)
        {
            this.mailSettings = mailSettings;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string content)
        {
            var email = new MimeMessage();

            email.Sender = MailboxAddress.Parse(this.mailSettings.Value.Mail);
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;

            var builder = new BodyBuilder();

            builder.HtmlBody = content;
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();

            smtp.Connect(this.mailSettings.Value.Host, this.mailSettings.Value.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(this.mailSettings.Value.Mail, this.mailSettings.Value.Password);

            await smtp.SendAsync(email);

            smtp.Disconnect(true);
        }
    }
}
