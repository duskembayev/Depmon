using Depmon.Server.Domain.Model;

namespace Depmon.Server.Domain
{
    public class Group : FactObject
    {
        public override FactObjectType Type => FactObjectType.Group;
    }
}