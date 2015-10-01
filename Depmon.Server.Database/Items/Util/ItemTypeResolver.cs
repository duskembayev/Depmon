using System;
using System.Collections.Generic;
using System.Linq;
using Depmon.Server.Domain;

namespace Depmon.Server.Database.Items.Util
{
    public class ItemTypeResolver : IItemTypeResolver
    {
        private static readonly Dictionary<ItemType, string> Map = new Dictionary<ItemType, string>();
        private static readonly Dictionary<Type, ItemType> MapType = new Dictionary<Type, ItemType>(); 

        static ItemTypeResolver()
        {
            Map.Add(ItemType.Source, "SourceCode");
            Map.Add(ItemType.Group, "GroupCode");
            Map.Add(ItemType.Resource, "ResourceCode");
            Map.Add(ItemType.Indicator, "IndicatorCode");

            MapType.Add(typeof (Source), ItemType.Source);
            MapType.Add(typeof (Group), ItemType.Group);
            MapType.Add(typeof (Resource), ItemType.Resource);
            MapType.Add(typeof (Indicator), ItemType.Indicator);
        }

        public string ResolveField(ItemType itemType)
        {
            return Map[itemType];
        }

        public ItemType ResolveEnum<T>()
        {
            return MapType[typeof (T)];
        }

        public ItemType[] ResolveParent(ItemType itemType)
        {
            return Enum
                .GetValues(typeof (ItemType))
                .Cast<ItemType>()
                .Where(t => t < itemType)
                .ToArray();
        }
    }
}