using System.Collections.Generic;

namespace Depmon.Server.Services
{
    public class NotificationSenderInfo:IData
    {
        public string Body { get; set; }
        public string Subject { get; set; }
        public string Sender { get; set; }
        public IList<string> Recipient { get; set; }
    }
}