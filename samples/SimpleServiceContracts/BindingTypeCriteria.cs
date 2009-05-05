using NIntegrate.Query.SqlClient;

namespace SimpleServiceContracts
{
    public sealed class BindingTypeCriteria : SqlCriteria
    {
        public BindingTypeCriteria()
            : base("BindingType_lkp", "Sample - SimpleService")
        {
        }

        public SqlInt32Column BindingType_id = new SqlInt32Column("BindingType_id");
        public SqlStringColumn BindingTypeFriendlyName = new SqlStringColumn("BindingTypeFriendlyName", false);
    }
}
