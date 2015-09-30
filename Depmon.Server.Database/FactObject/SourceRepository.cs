using System;
using Depmon.Server.Database.FactObject.Util;
using Depmon.Server.Domain;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database.FactObject
{
    public class SourceRepository : FactObjectRepository<Source>
    {
        public SourceRepository(IUnitOfWork unitOfWork, IFactObjectResolver resolver) : base(unitOfWork, resolver)
        {
        }

        protected override string GetParentFieldValue(FactObjectType field)
        {
            throw new NotSupportedException();
        }
    }
}