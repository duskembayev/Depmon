using Depmon.Server.Domain.Model;

namespace Depmon.Server.Domain
{
    public class Resource : FactObject
    {
        public override FactObjectType Type => FactObjectType.Resource;
    }
}