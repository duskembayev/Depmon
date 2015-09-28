using System.IO;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Collector
{
    public interface ICsvParse
    {
        Fact[] Parse(Stream data);
    }
}