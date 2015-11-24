using System;

namespace Depmon.Server.Domain
{
    public class Report
    {
        public int Id { get; set; }

        public DateTime CreatedAt { get; set; }

        public string SourceCode { get; set; }

        public bool IsLast { get; set; }
    }
}