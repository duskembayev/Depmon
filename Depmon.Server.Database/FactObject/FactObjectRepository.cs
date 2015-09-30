using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Depmon.Server.Database.FactObject.Util;
using Depmon.Server.Domain.Model;

namespace Depmon.Server.Database.FactObject
{
    public abstract class FactObjectRepository<T> : IRepository<T> where T: Domain.FactObject
    {
        private IUnitOfWork _unitOfWork;
        private readonly IFactObjectResolver _resolver;

        protected FactObjectType FactObjectType { get; }
        protected string FactObjectField { get; }

        protected FactObjectRepository(IUnitOfWork unitOfWork, IFactObjectResolver resolver)
        {
            _unitOfWork = unitOfWork;
            _resolver = resolver;
            FactObjectType = _resolver.ResolveEnum<T>();
            FactObjectField = _resolver.ResolveField(FactObjectType);
        }

        public void Dispose()
        {
            _unitOfWork = null;
        }

        protected abstract string GetParentFieldValue(FactObjectType field);

        public IEnumerable<T> GetAll()
        {
            var sqlBuilder = new StringBuilder($@"
select distinct fact.[{FactObjectField}] Code, ass.[DisplayName] DisplayName, ass.[DisplayIcon] DisplayIcon
    from Facts fact 
    left join Associations ass on fact.[{FactObjectField}] = ass.[Code] and ass.[Type] = {FactObjectType} and ass.[Hidden] = 0");

            var parameters = BuildWhereSection(sqlBuilder);

            return _unitOfWork.Session.Query<T>(sqlBuilder.ToString(), (object) parameters);
        }

        #region not supported

        public T GetById(int id)
        {
            throw new NotSupportedException();
        }

        public void InsertMany(params T[] entities)
        {
            throw new NotSupportedException();
        }

        public int Save(T entity)
        {
            throw new NotSupportedException();
        }

        public void Delete(int id)
        {
            throw new NotSupportedException();
        }

        #endregion
        
        private dynamic BuildWhereSection(StringBuilder sqlBuilder)
        {
            var parentFields = _resolver.ResolveParent(FactObjectType);
            dynamic parameters = new {};
            for (var fieldIndex = 0; fieldIndex < parentFields.Length; fieldIndex++)
            {
                sqlBuilder.Append(fieldIndex == 0 ? " where " : " and ");

                var field = parentFields[fieldIndex];
                var fieldName = _resolver.ResolveField(field);
                var fieldValue = GetParentFieldValue(field);
                sqlBuilder.Append($"fact[{fieldName}] = @{fieldName}");
                parameters[fieldName] = fieldValue;
            }
            return parameters;
        }
    }
}