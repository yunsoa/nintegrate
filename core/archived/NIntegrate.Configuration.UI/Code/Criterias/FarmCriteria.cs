using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class FarmCriteria : CriteriaBase
    {
        public FarmCriteria() : base("Farm")
        {
        }

        public SqlInt32Column Farm_id = new SqlInt32Column("Farm_id");
        public SqlStringColumn FarmName = new SqlStringColumn("FarmName", false);
        public SqlStringColumn FarmAddress = new SqlStringColumn("FarmAddress", false);
        public SqlInt32Column Environment_id = new SqlInt32Column("Environment_id");
    }
}
