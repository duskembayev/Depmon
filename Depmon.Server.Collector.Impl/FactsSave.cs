using System;
using System.Linq;
using Depmon.Server.Database;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Collector.Impl
{
    public class FactsSave : IFactsSave
    {
        private IUnitOfWork _unitOfWork;
        private IRepository<Report> _reportRepository;
        private IRepository<Fact> _factRepository;

        public FactsSave(IUnitOfWork unitOfWork, IRepository<Report> reportRepository, IRepository<Fact> factRepository)
        {
            _unitOfWork = unitOfWork;
            _factRepository = factRepository;
            _reportRepository = reportRepository;

            _unitOfWork.SetRepository(_factRepository);
            _unitOfWork.SetRepository(_reportRepository);
        }

        public void Save(Fact[] facts)
        {
            try
            {
                using (var transaction = _unitOfWork.BeginTransaction())
                {
                    var report = new Report { CreatedAt = DateTime.Now };
                    _reportRepository.Save(report);
                    var reportId = _reportRepository.GetAll().FirstOrDefault(s => s.CreatedAt == report.CreatedAt).Id;

                    foreach (var fact in facts)
                        fact.ReportId = reportId;

                    _factRepository.InsertMany(facts);

                    transaction.Commit();
                }

                Console.WriteLine("{0} facts saved", facts.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }
    }
}
