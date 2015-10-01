using Depmon.Server.Domain.Model;

namespace Depmon.Server.Domain
{
    public class Source : FactObject
    {
        public override FactObjectType Type => FactObjectType.Source;
    }
}