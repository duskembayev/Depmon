﻿using System;
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
        private readonly IEmailService _emailService;
        private Settings _config;

        public NotificationService(IUnitOfWork unitOfWork, ISettingsReader settingsReader, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _config = settingsReader.Read();
        }

        public void EveryDay()
        {
            var recievers = _config.Notification.Recievers;

            IDictionary<string, LevelCount> sourceCodeData = new Dictionary<string, LevelCount>();
            
            var sql = GetSql(recievers);
          
            var data = _unitOfWork.Session.Query(sql);

            var groupedBySourceCode = data.GroupBy(d => d.SourceCode).Select(s => new
            {
                SourceCode = s.Key,
                LevelInfo = new LevelCount
                {
                    Level25 = s.Where(l => l.Level == 25).Select(c => (int)c.Count).SingleOrDefault(),
                    Level50 = s.Where(l => l.Level == 50).Select(c => (int)c.Count).SingleOrDefault(),
                    Level75 = s.Where(l => l.Level == 75).Select(c => (int)c.Count).SingleOrDefault(),
                    Level100 = s.Where(l => l.Level == 100).Select(c => (int)c.Count).SingleOrDefault(),
                    CreatedAt = s.Select(c => c.CreatedAt).First(),
                    CustomStyles = CustomStyles(s)
                }
            });
            
            foreach (var group in groupedBySourceCode)
            {
                sourceCodeData.Add(group.SourceCode, group.LevelInfo);
            }

            foreach (Reciever reciever in recievers)
            {
                var filteredSources = reciever.SourceList.Count != 0
                    ? sourceCodeData.Where(k => reciever.SourceList.Contains(k.Key)).ToList()
                    : sourceCodeData.ToList();
                
                SourceInfoEmailTemplate template = new SourceInfoEmailTemplate() {SourceCodeData = filteredSources};
                
                var emailBody = template.TransformText();

                SendEmail(emailBody, reciever.Name);

            }
           
        }

        private string GetSql(RecieverCollection recievers)
        {
            var includeAllCodes = IsIncludeAllCodes(recievers);
            string sql;
            

            if (includeAllCodes)
            {
                sql = QueryStore.ProblemCountForSource();
            }
            else
            {
                var codes = GetSourceCodesFromRecievers(recievers);
                sql = QueryStore.ProblemCountForSource(codes.ToList());
            }

            return sql;
        }

        private bool IsIncludeAllCodes(RecieverCollection recievers)
        {
            return recievers.Cast<Reciever>().Any(r => r.SourceList.Count == 0);
        }

        private IEnumerable<string> GetSourceCodesFromRecievers(RecieverCollection recievers)
        {
            return recievers.Cast<Reciever>().SelectMany(c => c.SourceList).Distinct();
        }

        private void SendEmail(string emailBody, string recipient)
        {
            try
            {
                var info = new NotificationSenderInfo
                {
                    Body = emailBody,
                    Recipient = recipient,
                    Subject = "Daily monitoring statistic"
                };
                _emailService.SendNotification(info);
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot send notifaction to {0}", recipient);
            }
            
        }

        private string CustomStyles(IGrouping<dynamic, dynamic> s)
        {
            var dateTime =s.Select( c=> c.CreatedAt).First();
            var oldReportThreshold = _config.Notification.EveryDay.OldReportThreshold;

            if ((DateTime.Now - dateTime).TotalHours > oldReportThreshold)
            {
                return "color: #dc143c;";
            }

            return string.Empty;
        }
    }

    public interface INotificationService
    {
        void EveryDay();
    }
}