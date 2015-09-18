using System;

namespace Depmon.Server.Domain.Model
{
    public class Fact
    {
        public int Id { get; set; }

        public DateTime CheckedAt { get; set; }

        public string SourceCode { get; set; }

        public string GroupCode { get; set; }

        public string ResourceCode { get; set; }

        public string IndicatorCode { get; set; }  

        public decimal IndicatorValue { get; set; }

        public string IndicatorDescription { get; set; }

        public FactLevel Level { get; set; }

        public long ReportId { get; set; }

        public Report Report { get; set; }
    }
}   
