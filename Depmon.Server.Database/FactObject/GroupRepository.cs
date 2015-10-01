using System;
using Depmon.Server.Database.FactObject.Util;
using Depmon.Server.Domain;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database.FactObject
{
    public class GroupRepository : FactObjectRepository<Group>
    {
        public GroupRepository(IUnitOfWork unitOfWork, IFactObjectResolver resolver) : base(unitOfWork, resolver)
        {
        }

        public string FilterSource { get; set; }

        protected override string GetParentFieldValue(FactObjectType field)
        {
            switch (field)
            {
                case FactObjectType.Source:
                    return FilterSource;
                default:
                    throw new NotSupportedException();
            }
        }
    }
}