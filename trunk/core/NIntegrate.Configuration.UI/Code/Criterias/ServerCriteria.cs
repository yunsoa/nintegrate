using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class ServerCriteria : CriteriaBase
    {
        public ServerCriteria() : base("Server")
        {
        }

        public SqlInt32Column Server_id = new SqlInt32Column("Server_id");
        public SqlStringColumn ServerName = new SqlStringColumn("ServerName", false);
        public SqlStringColumn ServerAddress = new SqlStringColumn("ServerAddress", false);
        public SqlInt32Column Farm_id = new SqlInt32Column("Farm_id");
    }
}
