using System;
using CsvHelper.TypeConversion;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Collector.Impl
{
    class LevelConverter : ILevelConverter
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
