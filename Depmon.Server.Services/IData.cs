using System.Collections.Generic;

namespace Depmon.Server.Services
{
    public interface IData
    {
        string Body { get; set; }
        string Subject { get; set; }
        string Sender { get; set; }
        IList<string> Recipient { get; set; }
    }
}