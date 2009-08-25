using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class ServiceHostTypeCriteria : CriteriaBase
    {
        public ServiceHostTypeCriteria() : base("ServiceHostType_lkp")
        {
        }

        public SqlInt32Column ServiceHostType_id = new SqlInt32Column("ServiceHostType_id");
        public SqlStringColumn ServiceHostTypeFriendlyName = new SqlStringColumn("ServiceHostTypeFriendlyName", false);
        public SqlStringColumn ServiceHostTypeClassName = new SqlStringColumn("ServiceHostTypeClassName", false);   
    }
}
