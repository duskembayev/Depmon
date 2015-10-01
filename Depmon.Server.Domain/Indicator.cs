using Depmon.Server.Domain.Model;

namespace Depmon.Server.Domain
{
    public class Indicator : FactObject
    {
        public override FactObjectType Type => FactObjectType.Indicator;
    }
}