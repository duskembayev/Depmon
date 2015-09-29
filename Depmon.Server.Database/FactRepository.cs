using System.Data;
using Dapper;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database
{
    public class FactRepository : Repository<Fact>
    {
        protected override string TableName => "Facts";

        public override void Save(IDbConnection session, Fact fact)
        {
            var sqlInsert = $@"INSERT INTO {TableName} (CheckedAt,  SourceCode,  GroupCode,  ResourceCode,  IndicatorCode,  IndicatorValue,  IndicatorDescription,  Level,  ReportId)
                                        VALUES  (@CheckedAt, @SourceCode, @GroupCode, @ResourceCode, @IndicatorCode, @IndicatorValue, @IndicatorDescription, @Level, @ReportId)";
            var sqlUpdate = $@"UPDATE {TableName}
                              SET CheckedAt = @CheckedAt,  SourceCode = @SourceCode,  GroupCode = @GroupCode,  ResourceCode = @ResourceCode,  IndicatorCode = @IndicatorCode,
                                  IndicatorValue = @IndicatorValue,  IndicatorDescription = @IndicatorDescription,  Level = @Level,  ReportId = @ReportId
                              WHERE Id = @Id";
            var sql = fact.Id == 0 ? sqlInsert : sqlUpdate;

            session.Execute(sql, fact);
        }

        public override void InsertMany(IDbConnection session, params Fact[] facts)
        {
            var sql = $@"INSERT INTO {TableName} (CheckedAt,  SourceCode,  GroupCode,  ResourceCode,  IndicatorCode,  IndicatorValue,  IndicatorDescription,  Level,  ReportId)
                                        VALUES  (@CheckedAt, @SourceCode, @GroupCode, @ResourceCode, @IndicatorCode, @IndicatorValue, @IndicatorDescription, @Level, @ReportId)";

            session.Execute(sql, facts);
        }
    }
}
