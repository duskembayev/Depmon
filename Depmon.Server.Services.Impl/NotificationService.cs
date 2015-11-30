using System.Collections.Generic;
using System.Linq;
using Dapper;
using Depmon.Server.Collector.Configuration;
using Depmon.Server.Database;
using Depmon.Server.Database.Queries;
using Depmon.Server.Services.Impl.Templates;

namespace Depmon.Server.Services.Impl
{
    public class NotificationService:INotificationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private Settings _config;

        public NotificationService(IUnitOfWork unitOfWork, ISettingsReader settingsReader)
        {
            _unitOfWork = unitOfWork;
            _config = settingsReader.Read();
        }

        public void EveryDay()
        {
            var recievers = _config.Notification.Recievers;

            IDictionary<string, LevelCount> sourceCodeData = new Dictionary<string, LevelCount>();
            
            var codes = recievers.Cast<Reciever>().SelectMany(c => c.SourceList).Distinct();

            var sql = QueryStore.ProblemCountForSource(codes.ToList());

            var data = _unitOfWork.Session.Query(sql);

            var groupedBySourceCode = data.GroupBy(d => d.SourceCode).Select(s => new
            {
                SourceCode = s.Key,
                LevelInfo = new LevelCount
                {
                    Level25 = s.Where(l => l.Level == 25).Select(c => (int)c.Count).SingleOrDefault(),
                    Level50 = s.Where(l => l.Level == 50).Select(c => (int)c.Count).SingleOrDefault(),
                    Level75 = s.Where(l => l.Level == 75).Select(c => (int)c.Count).SingleOrDefault(),
                    Level100 = s.Where(l => l.Level == 100).Select(c => (int)c.Count).SingleOrDefault()
                }
            });

            foreach (var group in groupedBySourceCode)
            {
                sourceCodeData.Add(group.SourceCode, group.LevelInfo);
            }


            foreach (Reciever reciever in recievers)
            {
                var filteredSources = sourceCodeData.Where(k => reciever.SourceList.Contains(k.Key)).ToList();

                SourceInfoEmailTemplate template = new SourceInfoEmailTemplate() {SourceCodeData = filteredSources};
                
                var emailBody = template.TransformText();

            }
           
        }
    }

    public interface INotificationService
    {
        void EveryDay();
    }
}
