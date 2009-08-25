using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class BehaviorCriteria : CriteriaBase
    {
        public BehaviorCriteria() : base("Behavior")
        {
        }

        public SqlInt32Column Behavior_id = new SqlInt32Column("Behavior_id");
        public SqlStringColumn BehaviorName = new SqlStringColumn("BehaviorName", false);
        public SqlStringColumn BehaviorXML = new SqlStringColumn("BehaviorXML", false);
        public SqlInt32Column BehaviorCategory_id = new SqlInt32Column("BehaviorCategory_id");
    }
}
