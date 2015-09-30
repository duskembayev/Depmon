using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper.Configuration;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Collector.Impl
{
    public class CsvReader : ICsvReader
    {
        public Fact[] Read(Stream data)
        {
            var csvConfig = new CsvConfiguration
            {
                Delimiter = "|",
                Encoding = Encoding.UTF8,
                HasHeaderRecord = true,
            };
            csvConfig.RegisterClassMap<FactMap>();
            csvConfig.CultureInfo = CultureInfo.InvariantCulture;

            using (var reader = new StreamReader(data, Encoding.UTF8))
            using (var parser = new CsvHelper.CsvReader(reader, csvConfig))
            {
                return parser.GetRecords<Fact>().ToArray();
            }
        }
    }
}