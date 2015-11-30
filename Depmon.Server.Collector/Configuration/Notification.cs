using System.Configuration;

namespace Depmon.Server.Collector.Configuration
{
    public class Notification: ConfigurationElement
    {
        [ConfigurationProperty("everyDay")]
        public EveryDay EveryDay => (EveryDay)base["everyDay"];

        [ConfigurationProperty("sender")]
        public Sender Sender => (Sender) base["sender"];

        [ConfigurationProperty("recievers")]
        public RecieverCollection Recievers => (RecieverCollection)base["recievers"];
    }
}
