using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Depmon.Server.Collector.Configuration;

namespace Depmon.Server.Collector.Impl.Configuration
{
    public class ConfigReader : IConfigReader
    {
        private const string CONFIG_FILE = "config.xml";

        public MonitoringSection Read()
        {
            MonitoringSection result = new MonitoringSection();
            result.Iteration = new Iteration();
            result.Mailboxes = new List<Mailbox>();

            XDocument doc = XDocument.Load(CONFIG_FILE);

            result.Iteration.Delay = TimeSpan.Parse(doc.Root.Element("iteration").Attribute("delay").Value);

            var mailboxes = doc.Root.Element("mailboxes").Elements();
            foreach (var item in mailboxes)
            {
                result.Mailboxes.Add(new Mailbox
                {
                    Name = item.Attribute("name").Value,
                    Capacity = int.Parse(item.Attribute("capacity").Value),
                    Delay = TimeSpan.Parse(item.Attribute("delay").Value),
                    Password = item.Attribute("password").Value,
                    Port = int.Parse(item.Attribute("port").Value),
                    Server = item.Attribute("server").Value,
                    Ssl = bool.Parse(item.Attribute("ssl").Value),
                    Timeout = int.Parse(item.Attribute("timeout").Value),
                    Username = item.Attribute("username").Value
                });
            }
            
            return result;
        }
    }
}
