using Depmon.Server.Domain;

namespace Depmon.Server.Services
{
    public interface INotificationService
    {
        void EveryDay();
        void SendNewReport(Report report, Fact[] facts);
    }
}