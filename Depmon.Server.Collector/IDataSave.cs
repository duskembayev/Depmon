using System.Collections.Generic;
using System.IO;

namespace Depmon.Server.Collector
{
    public interface IDataSave
    {
        void Save(IList<Stream> dataList);
    }
}
