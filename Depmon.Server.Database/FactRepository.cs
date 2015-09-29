using System.Data;
using Dapper;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database
{
    public class FactRepository : Repository<Fact>
    {
        public FactRepository()
        { }

        public override void Save(Fact fact)
        {
            var sqlInsert = @"INSERT INTO Facts (CheckedAt,  SourceCode,  GroupCode,  ResourceCode,  IndicatorCode,  IndicatorValue,  IndicatorDescription,  Level,  ReportId)
                                        VALUES  (@CheckedAt, @SourceCode, @GroupCode, @ResourceCode, @IndicatorCode, @IndicatorValue, @IndicatorDescription, @Level, @ReportId)";
            var sqlUpdate = @"UPDATE Facts
                              SET CheckedAt = @CheckedAt,  SourceCode = @SourceCode,  GroupCode = @GroupCode,  ResourceCode = @ResourceCode,  IndicatorCode = @IndicatorCode,
                                  IndicatorValue = @IndicatorValue,  IndicatorDescription = @IndicatorDescription,  Level = @Level,  ReportId = @ReportId
                              WHERE Id = @Id";
            var sql = fact.Id == 0 ? sqlInsert : sqlUpdate;
            
            _connection.Execute(sql, fact);
        }

        public override void InsertMany(params Fact[] facts)
        {
            var sql = @"INSERT INTO Facts (CheckedAt,  SourceCode,  GroupCode,  ResourceCode,  IndicatorCode,  IndicatorValue,  IndicatorDescription,  Level,  ReportId)
                                        VALUES  (@CheckedAt, @SourceCode, @GroupCode, @ResourceCode, @IndicatorCode, @IndicatorValue, @IndicatorDescription, @Level, @ReportId)";

            _connection.Execute(sql, facts);
        }
    }
}
