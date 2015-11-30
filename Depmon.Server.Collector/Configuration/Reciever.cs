using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Depmon.Server.Collector.Configuration
{
    public class Reciever: ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name => (string)base["name"];

        [ConfigurationProperty("sources")]
        public string Sources => (string) base["sources"];

        public IList<string> SourceList
        {
            get
            {
                var list = new List<string>(
                        Sources.Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim()));
                return list;
            }
        }
    }
}
