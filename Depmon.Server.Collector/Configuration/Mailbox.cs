using System;

namespace Depmon.Server.Collector.Configuration
{
    public class Mailbox
    {
        public string Name { get; set; }

        public string Server { get; set; }

        public int Port { get; set; }

        public bool Ssl { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public int Timeout { get; set; }

        public int Capacity { get; set; }

        public TimeSpan Delay { get; set; }
    }
}
