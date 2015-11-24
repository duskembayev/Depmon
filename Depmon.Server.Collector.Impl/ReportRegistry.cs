using System;
using Depmon.Server.Database;
using Depmon.Server.Domain;

namespace Depmon.Server.Collector.Impl
{
    public class ReportRegistry : IReportRegistry
    {
        private IRepository<Report> _reportRepository;
        private IRepository<Fact> _factRepository;
        private IUnitOfWork _unitOfWork;

        public ReportRegistry(IUnitOfWork unitOfWork, IRepository<Report> reportRepository, IRepository<Fact> factRepository)
        {
            _unitOfWork = unitOfWork;
            _factRepository = factRepository;
            _reportRepository = reportRepository;
        }

        public void Save(Fact[] facts)
        {
            if (facts.Length == 0 || string.IsNullOrWhiteSpace(facts[0].SourceCode))
            {
                Console.WriteLine("facts or SourceCode is empty");
                return;
            }
            try
            {
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    var report = new Report { Id = 0, CreatedAt = DateTime.Now, SourceCode = facts[0].SourceCode, IsLast = true };
                    var reportId = _reportRepository.Save(report);

                    foreach (var fact in facts)
                        fact.ReportId = reportId;

                    _factRepository.InsertMany(facts);

                    transaction.Commit();
                }

                Console.WriteLine("{0} facts saved", facts.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Report saving failed: {e.Message}");
            }
        }
    }
}
