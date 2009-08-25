using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class BindingTypeCriteria : CriteriaBase
    {
        public BindingTypeCriteria() : base("BindingType_lkp")
        {
        }

        public SqlInt32Column BindingType_id = new SqlInt32Column("BindingType_id");
        public SqlStringColumn BindingTypeFriendlyName = new SqlStringColumn("BindingTypeFriendlyName", false);
    }
}
