using System;
using System.Collections.Generic;
using System.Text;

namespace Depmon.Server.Database.Queries
{
    public static class QueryStore
    {
        public static string SourceQuery()
        {
            return @"select r.CreatedAt, r.SourceCode, sum(f.Level > 25) as ProblemCount from Reports r
left join Facts f on f.ReportId = r.Id
where r.IsLast = 1
group by r.SourceCode";
        }

        public static string SourceInfoQuery()
        {
            return @"select r.CreatedAt, r.SourceCode, f.GroupCode, f.ResourceCode, f.IndicatorCode, f.IndicatorValue, f.IndicatorDescription, f.Level  from Reports r
left join Facts f on f.ReportId = r.Id
where r.IsLast = 1";
        }

        public static string SourceInfoByCodeQuery()
        {
            return
                @"select r.CreatedAt, r.SourceCode, f.GroupCode, f.ResourceCode, f.IndicatorCode, f.IndicatorValue, f.IndicatorDescription, f.Level  from Reports r
left join Facts f on f.ReportId = r.Id
where r.IsLast = 1 and r.SourceCode = @sourceCode";
        }

        public static string IsNewReportExist()
        {
            return @"select count() as count from Reports r
where r.CreatedAt > @dateTime";
        }

        public static string ProblemCountForSource()
        {
            return @"select r.CreatedAt, r.SourceCode, f.Level, count() as Count  from Reports r
left join Facts f on f.ReportId = r.Id
where r.IsLast = 1
group by r.SourceCode, f.Level";
        }

        public static string ProblemCountForSource(IList<string> sources)
        {
            var placeHodler = MakePlaceholders(sources);

            return String.Format(@"select r.CreatedAt, r.SourceCode, f.Level, count() as Count  from Reports r
left join Facts f on f.ReportId = r.Id
where r.IsLast = 1 and r.SourceCode in ({0})
group by r.SourceCode, f.Level", placeHodler);
        }

        private static string MakePlaceholders(IList<string> sources)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < sources.Count; i++)
            {
                sb.AppendFormat("'{0}',", sources[i]);
            }

            return sb.ToString().TrimEnd(',');
        }
    }
}
