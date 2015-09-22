using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database
{
    public class ReportRepository : Repository<Report>
    {
        public ReportRepository(IDbConnection connection) : base(connection)
        { }

        public override IEnumerable<Report> GetAll()
        {
            var sql = "SELECT * FROM Reports";
            return Connection.Query<Report>(sql);
        }

        public override Report GetById(int id)
        {
            var sql = "SELECT * FROM Reports WHERE Facts.Id = @id";
            var query = Connection.Query<Report>(sql, new { id = id });
            return query.ElementAtOrDefault(0);
        }

        public override void Save(Report entity)
        {
            var sqlInsert = @"INSERT INTO Reports (CreatedAt)
                                        VALUES  (@CreatedAt)";

            var sqlUpdate = @"UPDATE Reports
                              SET CreatedAt = @CreatedAt
                              WHERE Id = @Id";
            var sql = entity.Id == 0 ? sqlInsert : sqlUpdate;

            Connection.Execute(sql, entity);
        }

        public override void Delete(Report entity)
        {
            var sql = "DELETE FROM Reports WHERE Id = @Id";

            Connection.Execute(sql, entity);
        }
    }
}