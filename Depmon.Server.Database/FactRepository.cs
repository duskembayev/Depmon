using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database
{
    public class FactRepository : Repository<Fact>
    {
        public FactRepository(IDbConnection connection) : base(connection)
        { }

        public override IEnumerable<Fact> GetAll()
        {
            var sql = "SELECT * FROM Facts";
            return Connection.Query<Fact>(sql);
        }

        public override Fact GetById(int id)
        {
            var sql = "SELECT * FROM Facts WHERE Facts.Id = @id";
            var query = Connection.Query<Fact>(sql, new { id = id });
            return query.ElementAtOrDefault(0);
        }

        public override void Save(Fact entity)
        {
            var sqlInsert = @"INSERT INTO Facts (CheckedAt,  SourceCode,  GroupCode,  ResourceCode,  IndicatorCode,  IndicatorValue,  IndicatorDescription,  Level,  ReportId)
                                        VALUES  (@CheckedAt, @SourceCode, @GroupCode, @ResourceCode, @IndicatorCode, @IndicatorValue, @IndicatorDescription, @Level, @ReportId)";
            var sqlUpdate = @"UPDATE Facts
                              SET CheckedAt = @CheckedAt,  SourceCode = @SourceCode,  GroupCode = @GroupCode,  ResourceCode = @ResourceCode,  IndicatorCode = @IndicatorCode,
                                  IndicatorValue = @IndicatorValue,  IndicatorDescription = @IndicatorDescription,  Level = @Level,  ReportId = @ReportId
                              WHERE Id = @Id";
            var sql = entity.Id == 0 ? sqlInsert : sqlUpdate;

            Connection.Execute(sql, entity);
        }

        public override void Delete(Fact entity)
        {
            var sql = "DELETE FROM Facts WHERE Id = @Id";

            Connection.Execute(sql, entity);
        }
    }
}
