using System.Configuration;

namespace Depmon.Server.Collector.Configuration
{
    public class Settings : ConfigurationSection
    {
        [ConfigurationProperty("mailboxes")]
        public MailboxCollection Mailboxes => (MailboxCollection)base["mailboxes"];

        [ConfigurationProperty("iteration")]
        public Iteration Iteration => (Iteration)base["iteration"];
    }
}
