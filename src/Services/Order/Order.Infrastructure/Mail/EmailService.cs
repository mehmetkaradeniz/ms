using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Order.Application.Contracts.Infrastructure;
using Order.Application.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Order.Infrastructure.Mail
{
    // SendGrid implementation of IEmailService
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
        {
            _emailSettings = emailSettings.Value;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<bool> SendMail(Email email)
        {
            var client = new SendGridClient(_emailSettings.ApiKey);

            var subject = email.Subject;
            var to = new EmailAddress(email.To);
            var emailBody = email.Body;

            var from = new EmailAddress()
            {
                Email = _emailSettings.FromAddress,
                Name = _emailSettings.FromName
            };

            var sendGridMessage = MailHelper.CreateSingleEmail(from, to, subject, emailBody, emailBody);
            var response = await client.SendEmailAsync(sendGridMessage);


            if (response.StatusCode == HttpStatusCode.Accepted || response.StatusCode == HttpStatusCode.OK)
            {
                _logger.LogInformation($"Email sent to {to.Email}.");
                return true;
            }

            _logger.LogError($"Email send to {to.Email} failed.");
            return false;
        }
    }
}
