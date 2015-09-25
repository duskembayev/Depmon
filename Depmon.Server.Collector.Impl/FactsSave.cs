using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Autofac;
using Depmon.Server.Database;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Collector.Impl
{
    public class FactsSave : IFactsSave
    {
        public void Save(IList<Stream> dataList)
        {
            try
            {
                foreach (var stream in dataList)
                {
                    var dtos = new CsvParse().Parse(stream);
                    if(!dtos.Any()) continue;

                    SaveFacts(dtos);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private static void SaveFacts(Fact[] dtos)
        {
            var container = new AutofacContainer().GetContainer();
            using (var scope = container.BeginLifetimeScope())
            {
                var uow = scope.Resolve<IUnitOfWork>();
                var reportRepository = scope.Resolve<IRepository<Report>>();
                uow.SetRepository(reportRepository);

                var report = new Report {CreatedAt = DateTime.Now};
                reportRepository.Save(report);
                var reportId = reportRepository.GetAll().FirstOrDefault(s => s.CreatedAt == report.CreatedAt).Id;

                foreach (var fact in dtos)
                    fact.ReportId = reportId;

                var factRepository = scope.Resolve<IRepository<Fact>>();
                uow.SetRepository(factRepository);
                factRepository.InsertMany(dtos);

                uow.CommitChanges();

                Console.WriteLine("{0} facts saved", dtos.Length);
            }
        }
    }
}
