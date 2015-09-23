using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using CsvHelper;
using CsvHelper.Configuration;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Collector.Impl
{
    public class CsvParse
    {
        public Fact[] Parse(Stream data)
        {
            var csvConfig = new CsvConfiguration
            {
                Delimiter = "|",
                Encoding = Encoding.UTF8,
                HasHeaderRecord = true,
            };
            csvConfig.RegisterClassMap<FactMap>();
            csvConfig.CultureInfo = CultureInfo.InvariantCulture;

            try
            {
                using (var reader = new StreamReader(data, Encoding.UTF8))
                using (var parser = new CsvReader(reader, csvConfig))
                {
                    return parser.GetRecords<Fact>().ToArray();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}