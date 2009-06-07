using System;
using System.Runtime.Serialization;

namespace NIntegrate.Query.SqlClient
{
    [DataContract]
    [KnownType("KnownTypes")]
    public abstract class SqlCriteria : Criteria
    {
        #region KnownTypes

        internal new static Type[] KnownTypes()
        {
            return QueryHelper.KnownTypes();
        }

        #endregion

        protected SqlCriteria(string tableName, string connectionStringName)
            : base(tableName, connectionStringName)
        {
        }

        public Criteria AddSortByRandom()
        {
            return SqlExtensionMethods.AddSortByRandom(this);
        }

        public DateTimeExpression GetCurrentDate()
        {
            return SqlExtensionMethods.GetCurrentDate(this);
        }

        public DateTimeExpression GetCurrentUtcDate()
        {
            return SqlExtensionMethods.GetCurrentUtcDate(this);
        }
    }
}
