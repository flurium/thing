using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid.Helpers.Mail;
using SendGrid;
using Thing.Infrastructure;

namespace Thing.Services
{
    public class EmailSenderService : IEmailSender
    {
        private readonly SendGridOptions _sendGridOptions;
        private readonly SendGridClient _sendGridClient;

        public EmailSenderService(IOptions<SendGridOptions> options)
        {
            _sendGridOptions = options.Value;
            _sendGridClient = new SendGridClient(_sendGridOptions.SendGridKey);
        }

        private async Task SendEmailAsyncPriv(string email, string subject, string htmlMessage)
        {
            var message = new SendGridMessage
            {
                From = new EmailAddress(_sendGridOptions.UserMail),
                Subject = subject,
                HtmlContent = htmlMessage
            };
            message.AddTo(email);
            var res = await _sendGridClient.SendEmailAsync(message);
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            await SendEmailAsyncPriv(email, subject, htmlMessage);
        }
    }
}