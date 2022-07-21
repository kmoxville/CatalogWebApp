using MimeKit;
using MailKit.Net.Smtp;
using CatalogWebApp.Utils.Options;
using Microsoft.Extensions.Options;
using Polly.Retry;
using Polly;
using Polly.Contrib.WaitAndRetry;

namespace CatalogWebApp.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly EmailOptions _emailOptions;
        private readonly AsyncRetryPolicy _retryPolicy;
        private readonly ILogger<IEmailService> _logger;

        public EmailService(IOptions<EmailOptions> emailOptions,
            ILogger<IEmailService> logger)
        {
            _emailOptions = emailOptions.Value;
            _logger = logger;

            var delay = Backoff.DecorrelatedJitterBackoffV2(
                medianFirstRetryDelay: TimeSpan.FromSeconds(_emailOptions.RetryPolicy.RetryTimeout), 
                retryCount: _emailOptions.RetryPolicy.RetryCount);

            _retryPolicy = Policy.Handle<Exception>()
                .WaitAndRetryAsync(
                    sleepDurations: delay,
                    onRetry: (exception, retryCount) =>
                    {
                        logger.LogError($"Oops, something very bad happened!\n" +
                            $"Message:{exception.Message}\n" +
                            $"on retry #{retryCount}");
                    });
        }

        public async Task SendEmailAsync(string email, string subject, string message, CancellationToken cancellationToken = default)
        {
            await _retryPolicy.ExecuteAsync(async () =>
            {
                // throw new Exception("test");
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(_emailOptions.Name, _emailOptions.Address));
                emailMessage.To.Add(new MailboxAddress("", email));
                emailMessage.Subject = subject;
                emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
                {
                    Text = message
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(_emailOptions.Host, _emailOptions.Port, false, cancellationToken);
                    await client.AuthenticateAsync(_emailOptions.User, _emailOptions.Password, cancellationToken);
                    await client.SendAsync(emailMessage, cancellationToken);

                    await client.DisconnectAsync(true);
                }
            });
        }
    }
}
