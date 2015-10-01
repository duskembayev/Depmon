using System;
using Depmon.Server.Database.FactObject.Util;
using Depmon.Server.Domain;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database.FactObject
{
    public class IndicatorRepository : FactObjectRepository<Indicator>
    {
        public IndicatorRepository(IUnitOfWork unitOfWork, IFactObjectResolver resolver) : base(unitOfWork, resolver)
        {
        }

        public string FilterSource { get; set; }

        public string FilterGroup { get; set; }

        public string FilterResource { get; set; }

        protected override string GetParentFieldValue(FactObjectType field)
        {
            switch (field)
            {
                case FactObjectType.Source:
                    return FilterSource;
                case FactObjectType.Group:
                    return FilterGroup;
                case FactObjectType.Resource:
                    return FilterResource;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}