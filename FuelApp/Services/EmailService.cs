using FuelApp.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;


namespace FuelApp.Services
{
    public class EmailService : IEmailService
    {
        private EmailServiceOptions _emailServiceOptions;
        public EmailService(IOptions<EmailServiceOptions> emailServiceOptions)
        {
            _emailServiceOptions = emailServiceOptions.Value;
        }

        public Task SendEmail(string emailTo, string subject, string message)
        {
            try
            {
                using (var client = new SmtpClient(_emailServiceOptions.MailServer, int.Parse(_emailServiceOptions.MailPort)))
                {
                    if (bool.Parse(_emailServiceOptions.UseSSL) == true)
                        client.EnableSsl = true;

                    if (!string.IsNullOrEmpty(_emailServiceOptions.UserId))
                        client.Credentials = new NetworkCredential(_emailServiceOptions.UserId, _emailServiceOptions.Password);

                    client.Send(new MailMessage("moelgaard@fiberflex.dk", emailTo, subject, message));
                }
            }
            catch(Exception e)
            {
                string tmp = e.ToString();
            }
            return Task.CompletedTask;
        }
    }
}
