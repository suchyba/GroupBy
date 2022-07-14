using GroupBy.Application.Design.Services;
using GroupBy.Application.Model.Mail;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupBy.Application.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings mailSettings;

        public EmailService(IOptions<EmailSettings> mailSettings)
        {
            this.mailSettings = mailSettings.Value;
        }
        public async Task<bool> SendEmailAsync(string recipentEmail, string subject, string body)
        {
            var client = new SendGridClient(mailSettings.ApiKey);

            var to = new EmailAddress(recipentEmail);

            var from = new EmailAddress
            {
                Email = mailSettings.FromAddress,
                Name = mailSettings.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, body, body);
            sendGridMessage.SetClickTracking(false, false);

            var response = await client.SendEmailAsync(sendGridMessage);

            if (response.StatusCode == System.Net.HttpStatusCode.Accepted
                || response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }
    }
}
