using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace HotelReservation.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string htmlBody);
        bool CanSend();
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailService> _log;

        public EmailService(IConfiguration config, ILogger<EmailService> log)
        {
            _config = config;
            _log = log;
        }

        public bool CanSend()
        {
            var pw = _config["EmailSettings:SenderPassword"];
            var from = _config["EmailSettings:SenderEmail"];
            return !string.IsNullOrWhiteSpace(pw) && !string.IsNullOrWhiteSpace(from);
        }

        public async Task SendEmailAsync(string to, string subject, string htmlBody)
        {
            var smtpServer = _config["EmailSettings:SmtpServer"];
            var port = int.Parse(_config["EmailSettings:Port"] ?? "587");
            var fromEmail = _config["EmailSettings:SenderEmail"];
            var password = _config["EmailSettings:SenderPassword"];

            if (string.IsNullOrWhiteSpace(fromEmail) || string.IsNullOrWhiteSpace(password))
            {
                _log.LogWarning("EmailService: credentials not configured. Email will not be sent.");
                
                return;
            }

            using var client = new SmtpClient(smtpServer, port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(fromEmail, password)
            };

            var mail = new MailMessage
            {
                From = new MailAddress(fromEmail),
                Subject = subject,
                Body = htmlBody,
                IsBodyHtml = true
            };
            mail.To.Add(to);

            await client.SendMailAsync(mail);
            _log.LogInformation("EmailService: email sent to {to}", to);
        }
    }
}
