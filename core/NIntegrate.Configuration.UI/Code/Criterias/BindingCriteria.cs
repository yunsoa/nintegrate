using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class BindingCriteria : CriteriaBase
    {
        public BindingCriteria() : base("Binding")
        {
        }

        public SqlInt32Column Binding_id = new SqlInt32Column("Binding_id");
        public SqlStringColumn BindingName = new SqlStringColumn("BindingName", false);
        public SqlInt32Column BindingType_id = new SqlInt32Column("BindingType_id");
        public SqlStringColumn BindingXML = new SqlStringColumn("BindingXML", false);
        public SqlBooleanColumn MexBindingEnabled = new SqlBooleanColumn("MexBindingEnabled");
        public SqlBooleanColumn AddMexBindingOnly = new SqlBooleanColumn("AddMexBindingOnly");
    }
}
