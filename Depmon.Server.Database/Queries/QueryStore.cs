namespace Depmon.Server.Database.Queries
{
    public static class QueryStore
    {
        public static string SourceQuery()
        {
            return @"select r.SourceCode, sum(f.Level > 25) as ProblemCount from Reports r
left join Facts f on f.ReportId = r.Id
where r.IsLast = 1
group by r.SourceCode";
        }

        public static string SourceInfoQuery()
        {
            return @"select r.SourceCode, f.GroupCode, f.ResourceCode, f.IndicatorCode, f.IndicatorValue, f.IndicatorDescription, f.Level  from Reports r
left join Facts f on f.ReportId = r.Id
where r.IsLast = 1";
        }

        public static string SourceInfoByCodeQuery()
        {
            return
                @"select r.SourceCode, f.GroupCode, f.ResourceCode, f.IndicatorCode, f.IndicatorValue, f.IndicatorDescription, f.Level  from Reports r
left join Facts f on f.ReportId = r.Id
where r.IsLast = 1 and r.SourceCode = @sourceCode";
        }
    }
}
