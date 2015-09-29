using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Depmon.Server.Collector.Configuration
{
    public interface ISettings
    {
        MonitoringSection Read();
    }
}
