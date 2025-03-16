using BusinessLayer.Interface;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace BusinessLayer.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void SendPasswordResetEmail(string toEmail, string resetToken)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Address Book API", "no-reply@addressbook.com"));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = "Password Reset Request";

            message.Body = new TextPart("plain")
            {
                Text = $"Use this token to reset your password: {resetToken}"
            };

            using (var client = new SmtpClient())
            {
                client.Connect(_configuration["Smtp:Host"], int.Parse(_configuration["Smtp:Port"]), false);
                client.Authenticate(_configuration["Smtp:Username"], _configuration["Smtp:Password"]);
                client.Send(message);
                client.Disconnect(true);
            }
        }
    }
}
