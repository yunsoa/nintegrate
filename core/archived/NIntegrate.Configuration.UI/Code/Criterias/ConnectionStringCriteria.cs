using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class ConnectionStringCriteria : CriteriaBase
    {
        public ConnectionStringCriteria() : base("ConnectionString")
        {
        }

        public SqlStringColumn Name = new SqlStringColumn("Name", false);
        public SqlStringColumn Value = new SqlStringColumn("Value", false);
        public SqlStringColumn ProviderName = new SqlStringColumn("ProviderName", false);
        public SqlInt32Column Environment_id = new SqlInt32Column("Environment_id");
    }
}
