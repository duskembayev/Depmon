using System.Collections.Generic;

namespace Depmon.Server.Collector.Configuration
{
    public class MonitoringSection
    {
        public IList<Mailbox> Mailboxes { get; set; }

        public Iteration Iteration { get; set; }
    }
}
