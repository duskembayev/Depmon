using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database.FactObject.Util
{
    public interface IFactObjectResolver
    {
        string ResolveField(FactObjectType factObjectType);

        FactObjectType ResolveEnum<T>();

        FactObjectType[] ResolveParent(FactObjectType factObjectType);
    }
}