using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class FarmAccessibilityCriteria : CriteriaBase
    {
        public FarmAccessibilityCriteria() : base("FarmAccessibility")
        {
        }

        public SqlInt32Column FarmAccessibility_id = new SqlInt32Column("FarmAccessibility_id");
        public SqlInt32Column ClientFarm_id = new SqlInt32Column("ClientFarm_id");
        public SqlInt32Column ServerFarm_id = new SqlInt32Column("ServerFarm_id");
        public SqlInt32Column ChannelType_id = new SqlInt32Column("ChannelType_id");
    }
}
