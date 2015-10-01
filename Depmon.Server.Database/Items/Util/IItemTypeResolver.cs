using Depmon.Server.Domain;

namespace Depmon.Server.Database.Items.Util
{
    public interface IItemTypeResolver
    {
        string ResolveField(ItemType itemType);

        ItemType ResolveEnum<T>();

        ItemType[] ResolveParent(ItemType itemType);
    }
}