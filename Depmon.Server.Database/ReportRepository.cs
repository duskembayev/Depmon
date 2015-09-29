using System.Data;
using Dapper;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database
{
    public class ReportRepository : Repository<Report>
    {
        protected override string TableName => "Reports";

        public override void Save(IDbConnection session, Report report)
        {
            var sqlInsert = $@"INSERT INTO {TableName} (CreatedAt)
                                        VALUES  (@CreatedAt)";

            var sqlUpdate = $@"UPDATE {TableName}
                              SET CreatedAt = @CreatedAt
                              WHERE Id = @Id";
            var sql = report.Id == 0 ? sqlInsert : sqlUpdate;

            session.Execute(sql, report);
        }

        public override void InsertMany(IDbConnection session, params Report[] reports)
        {
            var sql = $@"INSERT INTO {TableName} (CreatedAt)
                                        VALUES  (@CreatedAt)";

            session.Execute(sql, reports);
        }
    }
}
