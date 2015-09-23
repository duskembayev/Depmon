using System.Collections.Generic;
using System.IO;

namespace Depmon.Server.Collector
{
    public interface IFactsSave
    {
        void Save(IList<Stream> dataList);
    }
}
