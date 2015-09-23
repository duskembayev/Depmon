using CsvHelper.Configuration;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Collector.Impl
{
    public sealed class FactMap : CsvClassMap<Fact>
    {
        public FactMap()
        {
            Map(f => f.Id).Ignore();
            Map(f => f.CheckedAt);
            Map(f => f.SourceCode);
            Map(f => f.GroupCode);
            Map(f => f.ResourceCode);
            Map(f => f.IndicatorCode);
            Map(f => f.IndicatorValue);
            Map(f => f.IndicatorDescription);
            Map(f => f.Level).TypeConverter<LevelConverter>();
            Map(f => f.Report).Ignore();
            Map(f => f.ReportId).Ignore();
        }
    }
}
