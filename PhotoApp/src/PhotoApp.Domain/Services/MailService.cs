using Microsoft.Extensions.Configuration;
using PhotoApp.Domain.Interfaces.IServices;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoApp.Domain.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;

        public MailService(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var apiKey = this._configuration["SendGridKey"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("aslanteam01.noreply@gmail.com", "ADMIN");
            // var subject = "Sending with SendGrid is Fun";
            var to = new EmailAddress(toEmail);
            var plainTextContent = "and easy to do anywhere, even with C#";
            // var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, body);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
