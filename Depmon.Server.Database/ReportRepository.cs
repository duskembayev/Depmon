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
    }
}
