namespace Depmon.Server.Services
{
    public interface INotificationSender
    {
        void Send(NotificationSenderInfo info);
    }
}
