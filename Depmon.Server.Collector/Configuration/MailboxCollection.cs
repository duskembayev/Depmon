using System.Configuration;

namespace Depmon.Server.Collector.Configuration
{
    [ConfigurationCollection(typeof(Mailbox))]
    public class MailboxCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Mailbox();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Mailbox)element).Name;
        }

        public Mailbox this[int idx] => (Mailbox)BaseGet(idx);
    }
}
