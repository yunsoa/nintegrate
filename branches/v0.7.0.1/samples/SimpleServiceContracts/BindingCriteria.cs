using System.Runtime.Serialization;
using NIntegrate.Query.SqlClient;

namespace SimpleServiceContracts
{
    [DataContract]
    public sealed class BindingCriteria : SqlCriteria
    {
        public BindingCriteria()
            : base("Binding", "Sample - SimpleService")
        {
        }

        public SqlInt32Column Binding_id = new SqlInt32Column("Binding_id");
        public SqlInt32Column BindingType_id = new SqlInt32Column("BindingType_id");
        public SqlStringColumn BindingName = new SqlStringColumn("BindingName", false);
        public SqlStringColumn BindingXML = new SqlStringColumn("BindingXML", false);
        public SqlBooleanColumn MexBindingEnabled = new SqlBooleanColumn("MexBindingEnabled");
    }
}
