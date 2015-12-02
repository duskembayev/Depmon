namespace Depmon.Server.Services
{
    public interface IData
    {
        string Body { get; set; }
        string Subject { get; set; }
        string Recipient { get; set; }
    }
}