using System.Collections;
using System.Linq;
using System.Web.Http;
using Dapper;
using Depmon.Server.Database;
using Depmon.Server.Domain;

namespace Depmon.Server.Portal.Controllers
{
    public class DrilldownController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public DrilldownController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IEnumerable Sources()
        {
            var sql = @"
select r.SourceCode Code, f.Level Level, count(f.IndicatorCode) Count from Reports r
join Facts f on f.ReportId = r.Id
where r.IsLast = 1
group by r.SourceCode, f.Level";

            var data = _unitOfWork.Session.Query(sql);
            return data.GroupBy(row => row.Code).Select(source => new
            {
                Code = source.Key,
                Level = (FactLevel) source.Max(row => row.Level),
                AllCount = source.Sum(row => (int) row.Count),
                BugCount = source.Where(row => (int) row.Level > (int) FactLevel.Normal).Sum(row => (int) row.Count)
            }).ToList();
        }

        [HttpGet]
        public IEnumerable Groups(string sourceCode)
        {
            var sql = @"
select f.GroupCode Code, f.Level Level, count(f.IndicatorCode) Count from Reports r
join Facts f on f.ReportId = r.Id
where r.IsLast = 1 and r.SourceCode = @sourceCode
group by f.GroupCode, f.Level";

            var data = _unitOfWork.Session.Query(sql, new { sourceCode });
            return data.GroupBy(row => row.Code).Select(group => new
            {
                Code = group.Key,
                Level = (FactLevel) group.Max(row => row.Level),
                AllCount = group.Sum(row => (int) row.Count),
                BugCount = group.Where(row => (int) row.Level > (int) FactLevel.Normal).Sum(row => (int) row.Count)
            }).ToList();
        }

        [HttpGet]
        public IEnumerable Resources(string sourceCode, string groupCode)
        {
            var sql = @"
select f.ResourceCode Code, f.Level Level, count(f.IndicatorCode) Count from Reports r
join Facts f on f.ReportId = r.Id
where r.IsLast = 1 and r.SourceCode = @sourceCode and f.GroupCode = @groupCode
group by f.ResourceCode, f.Level";

            var data = _unitOfWork.Session.Query(sql, new { sourceCode, groupCode });
            return data.GroupBy(row => row.Code).Select(resource => new
            {
                Code = resource.Key,
                Level = (FactLevel) resource.Max(row => row.Level),
                AllCount = resource.Sum(row => (int) row.Count),
                BugCount = resource.Where(row => (int) row.Level > (int) FactLevel.Normal).Sum(row => (int) row.Count)
            }).ToList();
        }

        [HttpGet]
        public IEnumerable Indicators(string sourceCode, string groupCode, string resourceCode)
        {
            var sql = @"
select f.IndicatorCode Code, f.Level Level, f.IndicatorValue, f.IndicatorDescription from Reports r
join Facts f on f.ReportId = r.Id
where r.IsLast = 1 and r.SourceCode = @sourceCode and f.GroupCode = @groupCode and f.ResourceCode = @resourceCode";

            var data = _unitOfWork.Session.Query(sql, new { sourceCode, groupCode, resourceCode });
            return data.ToList();
        }
    }
}
