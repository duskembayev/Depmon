namespace Depmon.Server.Services.Impl
{
    public class EmailNotificationSender : INotificationSender
    {
        private readonly IEmailService _emailService;

        public EmailNotificationSender(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public void Send(NotificationSenderInfo info)
        {
            _emailService.SendNotification(info);
        }
    }
}