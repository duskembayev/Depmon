using Depmon.Server.Domain.Model;

namespace Depmon.Server.Domain
{
    public abstract class FactObject
    {
        public abstract FactObjectType Type { get; }

        public string Code { get; set; }

        public string DisplayName { get; set; }

        public string DisplayIcon { get; set; }
    }
}