using System;

namespace Depmon.Server.Services
{
    public class ReportInfo
    {
        public int Level25 { get; set; }
        public int Level50 { get; set; }
        public int Level75 { get; set; }
        public int Level100 { get; set; }
        public DateTime CreatedAt { get; set; }

        public string CustomStyles { get; set; }
    }
}
