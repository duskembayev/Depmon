using System;
using System.Data;
using Dapper;

namespace Depmon.Server.Database
{
    public class ReportRepository : RepositoryBase
    {
        public ReportRepository(IDbConnection connection) : base(connection)
        {
        }

        public int Create()
        {
            var sql = @"insert into Reports (CreatedAt) values (@date)";
            var prm = new {date = DateTime.Now};

            Connection.Execute(sql, prm);
            return LastId;
        }
    }
}