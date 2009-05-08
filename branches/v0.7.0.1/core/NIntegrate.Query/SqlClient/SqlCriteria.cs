using System.Runtime.Serialization;

namespace NIntegrate.Query.SqlClient
{
    [DataContract]
    public abstract class SqlCriteria : Criteria
    {
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
