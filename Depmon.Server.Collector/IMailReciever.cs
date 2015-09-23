using System.Collections.Generic;
using System.IO;
using Depmon.Server.Collector.Configuration;

namespace Depmon.Server.Collector
{
    public interface IDataLoad
    {
        IList<Stream> Load(Mailbox mailbox);
    }
}
