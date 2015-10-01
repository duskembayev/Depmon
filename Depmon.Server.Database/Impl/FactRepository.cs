using System.Linq;
using Dapper;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database.Impl
{
    public class FactRepository : Repository<Fact>
    {
        protected override string TableName => "Facts";

        public FactRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public override int Save(Fact fact)
        {
            var sqlInsert = $@"INSERT INTO {TableName} (CheckedAt,  SourceCode,  GroupCode,  ResourceCode,  IndicatorCode,  IndicatorValue,  IndicatorDescription,  Level,  ReportId)
                                        VALUES  (@CheckedAt, @SourceCode, @GroupCode, @ResourceCode, @IndicatorCode, @IndicatorValue, @IndicatorDescription, @Level, @ReportId);
                                        select last_insert_rowid()";

            var sqlUpdate = $@"UPDATE {TableName}
                              SET CheckedAt = @CheckedAt,  SourceCode = @SourceCode,  GroupCode = @GroupCode,  ResourceCode = @ResourceCode,  IndicatorCode = @IndicatorCode,
                                  IndicatorValue = @IndicatorValue,  IndicatorDescription = @IndicatorDescription,  Level = @Level,  ReportId = @ReportId
                              WHERE Id = @Id";

            if (fact.Id == 0)
            {
                return _unitOfWork.Session.Query<int>(sqlInsert, fact).FirstOrDefault();
            }

            _unitOfWork.Session.Execute(sqlUpdate, fact);
            return fact.Id;
        }

        public override void InsertMany(params Fact[] facts)
        {
            var sql = $@"INSERT INTO {TableName} (CheckedAt,  SourceCode,  GroupCode,  ResourceCode,  IndicatorCode,  IndicatorValue,  IndicatorDescription,  Level,  ReportId)
                                        VALUES  (@CheckedAt, @SourceCode, @GroupCode, @ResourceCode, @IndicatorCode, @IndicatorValue, @IndicatorDescription, @Level, @ReportId)";

            _unitOfWork.Session.Execute(sql, facts);
        }
    }
}
