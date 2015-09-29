using System;
using Depmon.Server.Database;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Collector.Impl
{
    public class ReportRegistry : IReportRegistry
    {
        private IRepository<Report> _reportRepository;
        private IRepository<Fact> _factRepository;

        public ReportRegistry(IRepository<Report> reportRepository, IRepository<Fact> factRepository)
        {
            _factRepository = factRepository;
            _reportRepository = reportRepository;
        }

        public void Save(Fact[] facts)
        {
            try
            {
                var report = new Report {Id = 0, CreatedAt = DateTime.Now};
                var reportId = _reportRepository.Save(report);
                
                foreach (var fact in facts)
                    fact.ReportId = reportId;

                _factRepository.InsertMany(facts);

                Console.WriteLine("{0} facts saved", facts.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Report saving failed: {e.Message}");
            }
        }
    }
}
