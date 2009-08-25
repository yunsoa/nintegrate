using System;
using System.Runtime.Serialization;

namespace NIntegrate.Query.OracleClient
{
    [DataContract]
    [KnownType("KnownTypes")]
    public abstract class OracleCriteria : Criteria
    {
        #region KnownTypes

        internal new static Type[] KnownTypes()
        {
            return QueryHelper.KnownTypes();
        }

        #endregion

        protected OracleCriteria(string tableName, string connectionStringName)
            : base(tableName, connectionStringName)
        {
        }

        public DateTimeExpression GetCurrentDate()
        {
            return OracleExtensionMethods.GetCurrentDate(this);
        }
    }
}
