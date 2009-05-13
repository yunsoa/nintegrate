using System.Runtime.Serialization;
using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    [DataContract]
    public abstract class CriteriaBase : SqlCriteria
    {
        protected CriteriaBase(string tableName)
            : base(tableName, Constants.ConfigurationDatabaseConnectionStringName)
        {
        }
    }
}
