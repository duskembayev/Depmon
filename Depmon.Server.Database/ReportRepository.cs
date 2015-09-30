using System.Linq;
using Dapper;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database
{
    public class ReportRepository : Repository<Report>
    {
        protected override string TableName => "Reports";

        public ReportRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public override int Save(Report report)
        {
            var sqlInsert = $@"INSERT INTO {TableName} (CreatedAt)
                                        VALUES  (@CreatedAt);
                                        SELECT last_insert_rowid()";

            var sqlUpdate = $@"UPDATE {TableName}
                              SET CreatedAt = @CreatedAt
                              WHERE Id = @Id";
            
            if (report.Id == 0)
            {
                return _unitOfWork.Session.Query<int>(sqlInsert, report).FirstOrDefault();
            }

            _unitOfWork.Session.Execute(sqlUpdate, report);
            return report.Id;
        }

        public override void InsertMany(params Report[] reports)
        {
            var sql = $@"INSERT INTO {TableName} (CreatedAt)
                                        VALUES  (@CreatedAt)";

            _unitOfWork.Session.Execute(sql, reports);
        }
    }
}
