using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using VoBoo.Models;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace VoBoo.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings emailSettings;

        public EmailSender(IOptions<EmailSettings> emailSettings)
        {
            this.emailSettings = emailSettings.Value;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                var credentials = new NetworkCredential(emailSettings.Sender, emailSettings.Password);

                var mail = new MailMessage()
                {
                    From = new MailAddress(emailSettings.Sender, emailSettings.SenderName),
                    Subject = subject,
                    Body = message,
                    IsBodyHtml = true
                };

                mail.To.Add(new MailAddress(email));

                var client = new SmtpClient()
                {
                    Port = emailSettings.MailPort,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = emailSettings.MailServer,
                    EnableSsl = true,
                    Credentials = credentials
                };

                client.Send(mail);
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }

            return Task.CompletedTask;
        }
    }
}
