using System;
using System.Collections.Generic;
using System.Linq;
using Depmon.Server.Domain;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database.FactObject.Util
{
    public class FactObjectResolver : IFactObjectResolver
    {
        private static readonly Dictionary<FactObjectType, string> Map = new Dictionary<FactObjectType, string>();
        private static readonly Dictionary<Type, FactObjectType> MapType = new Dictionary<Type, FactObjectType>(); 

        static FactObjectResolver()
        {
            Map.Add(FactObjectType.Source, "SourceCode");
            Map.Add(FactObjectType.Group, "GroupCode");
            Map.Add(FactObjectType.Resource, "ResourceCode");
            Map.Add(FactObjectType.Indicator, "IndicatorCode");

            MapType.Add(typeof (Source), FactObjectType.Source);
            MapType.Add(typeof (Group), FactObjectType.Group);
            MapType.Add(typeof (Resource), FactObjectType.Resource);
            MapType.Add(typeof (Indicator), FactObjectType.Indicator);
        }

        public string ResolveField(FactObjectType factObjectType)
        {
            return Map[factObjectType];
        }

        public FactObjectType ResolveEnum<T>()
        {
            return MapType[typeof (T)];
        }

        public FactObjectType[] ResolveParent(FactObjectType factObjectType)
        {
            return Enum
                .GetValues(typeof (FactObjectType))
                .Cast<FactObjectType>()
                .Where(t => t < factObjectType)
                .ToArray();
        }
    }
}