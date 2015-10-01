using System;
using System.Linq;
using Dapper;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database.Impl
{
    public class ReportRepository : Repository<Report>
    {
        protected override string TableName => "Reports";

        public ReportRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public override int Save(Report report)
        {
            var sqlInsert = $@"INSERT INTO {TableName} (CreatedAt, SourceCode, IsLast)
                                        VALUES  (@CreatedAt, @SourceCode, @IsLast);
                                        SELECT last_insert_rowid()";

            var sqlUpdate = $@"UPDATE {TableName}
                              SET CreatedAt = @CreatedAt,
                                  SourceCode = @SourceCode,
                                  IsLast = @IsLast
                              WHERE Id = @Id";
            
            if (report.Id == 0)
            {
                var transaction = _unitOfWork.BeginTransaction();
                try
                {
                    UpdateOlderReports(report.SourceCode);
                    var reportId = _unitOfWork.Session.Query<int>(sqlInsert, report).FirstOrDefault();
                    transaction.Commit();
                    return reportId;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            _unitOfWork.Session.Execute(sqlUpdate, report);
            return report.Id;
        }

        private void UpdateOlderReports(string sourceCode)
        {
            var sql = $@"
UPDATE {TableName}
SET IsLast = 'false'
WHERE SourceCode = '{sourceCode}'";
            _unitOfWork.Session.Execute(sql);
        }

        public override void InsertMany(params Report[] reports)
        {
            var sql = $@"INSERT INTO {TableName} (CreatedAt)
                                        VALUES  (@CreatedAt)";

            _unitOfWork.Session.Execute(sql, reports);
        }
    }
}
