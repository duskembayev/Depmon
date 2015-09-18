using System.Data;
using Dapper;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database
{
    public class FactRepository : RepositoryBase
    {
        public FactRepository(IDbConnection connection) : base(connection)
        {
        }

        public void InsertMany(params Fact[] data)
        {
            var sql = @"
insert into Facts (CheckedAt, SourceCode, GroupCode, ResourceCode, IndicatorCode, IndicatorValue, IndicatorDescription, Level, ReportId)
values (@CheckedAt, @SourceCode, @GroupCode, @ResourceCode, @IndicatorCode, @IndicatorValue, @IndicatorDescription, @Level, @ReportId)";

            Connection.Execute(sql, data);
        }
    }
}