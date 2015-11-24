using System;
using CsvHelper.Configuration;
using CsvHelper.TypeConversion;
using Depmon.Server.Domain;

namespace Depmon.Server.Collector.Impl
{
    public sealed class FactMap : CsvClassMap<Fact>
    {
        public FactMap()
        {
            Map(f => f.Id).Ignore();
            Map(f => f.Report).Ignore();
            Map(f => f.ReportId).Ignore();
            Map(f => f.SourceCode);
            Map(f => f.GroupCode);
            Map(f => f.ResourceCode);
            Map(f => f.IndicatorCode);
            Map(f => f.IndicatorValue);
            Map(f => f.IndicatorDescription);
            Map(f => f.Level).TypeConverter<LevelConverter>();
            Map(f => f.CheckedAt);
        }

        internal class LevelConverter : ITypeConverter
        {
            public string ConvertToString(TypeConverterOptions options, object value)
            {
                return ((FactLevel)value).ToString("G");
            }

            public object ConvertFromString(TypeConverterOptions options, string text)
            {
                return Enum.Parse(typeof(FactLevel), text);
            }

            public bool CanConvertFrom(Type type)
            {
                return true;
            }

            public bool CanConvertTo(Type type)
            {
                return true;
            }
        }
    }
}
