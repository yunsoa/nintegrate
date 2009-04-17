using System.Runtime.Serialization;
using NIntegrate.Query;

namespace SimpleServiceContracts
{
    [DataContract]
    public sealed class BindingCriteria : Criteria
    {
        public BindingCriteria()
            : base("Binding", "Sample - SimpleService")
        {
        }

        public Int32Column Binding_id = new Int32Column("Binding_id");
        public Int32Column BindingType_id = new Int32Column("BindingType_id");
        public StringColumn BindingName = new StringColumn("BindingName", false);
        public StringColumn BindingXML = new StringColumn("BindingXML", false);
        public BooleanColumn MexBindingEnabled = new BooleanColumn("MexBindingEnabled");
    }
}
