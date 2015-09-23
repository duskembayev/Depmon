using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Autofac;
using CsvHelper;
using CsvHelper.Configuration;
using Depmon.Server.Database;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Collector.Impl
{
    public class DataSave : IDataSave
    {
        public void Save(IList<Stream> dataList)
        {
            try
            {
                IContainer continer = new AutofacContainer().GetContainer();

                using (var scope = continer.BeginLifetimeScope())
                {
                    using (var unitOfWork = scope.Resolve<IUnitOfWork>())
                    using (var rRepo = unitOfWork.GetRepository<Report>())
                    using (var fRepo = unitOfWork.GetRepository<Fact>())
                    {
                        foreach (var stream in dataList)
                        {
                            var report = new Report {CreatedAt = DateTime.Now};
                            rRepo.Save(report);
                            var reportId = rRepo.GetAll().FirstOrDefault(s => s.CreatedAt == report.CreatedAt).Id;

                            Process(stream, reportId, fRepo);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private void Process(Stream data, long reportId, IRepository<Fact> factRepository)
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
                    var dtos = parser.GetRecords<Fact>().ToArray();
                    foreach (var fact in dtos)
                    {
                        fact.ReportId = reportId;
                    }

                    factRepository.InsertMany(dtos);

                    Console.WriteLine("{0} facts saved", dtos.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("CSV parsing error: {0}", e.Message);
            }
        }
    }
}
