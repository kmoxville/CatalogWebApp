using CatalogWebApp.Services.EmailService;
using CatalogWebApp.Utils.Options;
using Microsoft.Extensions.Options;
using Quartz;

namespace CatalogWebApp.Services.NotificationService
{
    public class NotificationService : INotificationService, IJob
    {
        private readonly ILogger<INotificationService> _logger;
        private readonly IEmailService _emailService;
        private readonly NotificationEmailOptions _options;

        public NotificationService(ILogger<INotificationService> logger,
            IEmailService emailService, IOptions<NotificationEmailOptions> options)
        {
            _logger = logger;
            _emailService = emailService;
            _options = options.Value;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            await _emailService.SendEmailAsync(_options.Address, "I am still alive", "...");
        }
    }
}
