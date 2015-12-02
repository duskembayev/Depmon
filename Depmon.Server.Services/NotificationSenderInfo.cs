namespace Depmon.Server.Services
{
    public class NotificationSenderInfo:IData
    {
        public string Body { get; set; }
        public string Subject { get; set; }
        public string Recipient { get; set; }
    }
}