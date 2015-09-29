using Dapper;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database
{
    public class ReportRepository : Repository<Report>
    {
        public override void Save(Report report)
        {
            var sqlInsert = @"INSERT INTO Reports (CreatedAt)
                                        VALUES  (@CreatedAt)";

            var sqlUpdate = @"UPDATE Reports
                              SET CreatedAt = @CreatedAt
                              WHERE Id = @Id";
            var sql = report.Id == 0 ? sqlInsert : sqlUpdate;

            _connection.Execute(sql, report);
        }

        public override void InsertMany(params Report[] reports)
        {
            var sql = @"INSERT INTO Reports (CreatedAt)
                                        VALUES  (@CreatedAt)";

            _connection.Execute(sql, reports);
        }
    }
}
