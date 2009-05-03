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
            return ExtensionMethods.AddSortByRandom(this);
        }

        public DateTimeExpression GetCurrentDate()
        {
            return ExtensionMethods.GetCurrentDate(this);
        }

        public DateTimeExpression GetCurrentUtcDate()
        {
            return ExtensionMethods.GetCurrentUtcDate(this);
        }
    }
}
