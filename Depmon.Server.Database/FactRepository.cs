using Dapper;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database
{
    public class FactRepository : Repository<Fact>
    {
        protected override string TableName => "Facts";

        public FactRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        { }

        public override void Save(Fact fact)
        {
            var sqlInsert = $@"INSERT INTO {TableName} (CheckedAt,  SourceCode,  GroupCode,  ResourceCode,  IndicatorCode,  IndicatorValue,  IndicatorDescription,  Level,  ReportId)
                                        VALUES  (@CheckedAt, @SourceCode, @GroupCode, @ResourceCode, @IndicatorCode, @IndicatorValue, @IndicatorDescription, @Level, @ReportId)";
            var sqlUpdate = $@"UPDATE {TableName}
                              SET CheckedAt = @CheckedAt,  SourceCode = @SourceCode,  GroupCode = @GroupCode,  ResourceCode = @ResourceCode,  IndicatorCode = @IndicatorCode,
                                  IndicatorValue = @IndicatorValue,  IndicatorDescription = @IndicatorDescription,  Level = @Level,  ReportId = @ReportId
                              WHERE Id = @Id";
            var sql = fact.Id == 0 ? sqlInsert : sqlUpdate;

            _unitOfWork.Session.Execute(sql, fact);
        }

        public override void InsertMany(params Fact[] facts)
        {
            var sql = $@"INSERT INTO {TableName} (CheckedAt,  SourceCode,  GroupCode,  ResourceCode,  IndicatorCode,  IndicatorValue,  IndicatorDescription,  Level,  ReportId)
                                        VALUES  (@CheckedAt, @SourceCode, @GroupCode, @ResourceCode, @IndicatorCode, @IndicatorValue, @IndicatorDescription, @Level, @ReportId)";

            _unitOfWork.Session.Execute(sql, facts);
        }
    }
}
