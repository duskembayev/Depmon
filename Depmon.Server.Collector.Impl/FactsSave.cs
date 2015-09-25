using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Depmon.Server.Database;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Collector.Impl
{
    public class FactsSave : IFactsSave
    {
        private IUnitOfWork _unitOfWork;

        public FactsSave(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Save(IList<Stream> dataList)
        {
            try
            {
                using (var rRepo = _unitOfWork.GetRepository<Report>())
                using (var fRepo = _unitOfWork.GetRepository<Fact>())
                {
                    foreach (var stream in dataList)
                    {
                        var report = new Report {CreatedAt = DateTime.Now};
                        rRepo.Save(report);
                        var reportId = rRepo.GetAll().FirstOrDefault(s => s.CreatedAt == report.CreatedAt).Id;

                        Process(stream, reportId, fRepo);
                    }

                    _unitOfWork.CommitChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private void Process(Stream data, long reportId, IRepository<Fact> factRepository)
        {
            try
            {
                    var dtos = new CsvParse().Parse(data);
                    foreach (var fact in dtos)
                    {
                        fact.ReportId = reportId;
                    }

                    factRepository.InsertMany(dtos);

                    Console.WriteLine("{0} facts saved", dtos.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("CSV parsing error: {0}", e.Message);
            }
        }
    }
}
