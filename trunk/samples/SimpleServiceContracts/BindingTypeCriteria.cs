using NIntegrate.Query;

namespace SimpleServiceContracts
{
    public sealed class BindingTypeCriteria : Criteria
    {
        public BindingTypeCriteria()
            : base("BindingType_lkp", "Sample - SimpleService")
        {
        }

        public Int32Column BindingType_id = new Int32Column("BindingType_id");
        public StringColumn BindingTypeFriendlyName = new StringColumn("BindingTypeFriendlyName", false);
    }
}
