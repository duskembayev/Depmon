using System.IO;
using Depmon.Server.Domain;

namespace Depmon.Server.Collector
{
    public interface ICsvReader
    {
        Fact[] Read(Stream data);
    }
}