using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class CustomBehaviorTypeCriteria : CriteriaBase
    {
        public CustomBehaviorTypeCriteria()
            : base("CustomBehaviorType_lkp")
        {
        }

        public SqlInt32Column BehaviorType_id = new SqlInt32Column("BehaviorType_id");
        public SqlStringColumn BehaviorTypeExtensionName = new SqlStringColumn("BehaviorTypeExtensionName", false);
        public SqlStringColumn BehaviorTypeFriendlyName = new SqlStringColumn("BehaviorTypeFriendlyName", false);
        public SqlStringColumn BehaviorTypeClassName = new SqlStringColumn("BehaviorTypeClassName", false);
        public SqlStringColumn BehaviorConfigurationElementTypeClassName = new SqlStringColumn("BehaviorConfigurationElementTypeClassName", false);
        public SqlInt32Column BehaviorCategory_id = new SqlInt32Column("BehaviorCategory_id");
    }
}
