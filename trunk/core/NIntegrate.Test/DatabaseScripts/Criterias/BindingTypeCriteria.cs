using NIntegrate.Query;
using System.Runtime.Serialization;

namespace NIntegrate.Test.DatabaseScripts.Criterias
{
    [DataContract]
    public sealed class BindingTypeCriteria : Criteria
    {
        public BindingTypeCriteria()
            : base("BindingType_lkp", "NIntegrateConfig")
        {
        }

        public Int32Column BindingType_id = new Int32Column("BindingType_id");
        public StringColumn BindingTypeFriendlyName = new StringColumn("BindingTypeFriendlyName", false);
        public StringColumn BindingTypeClassName = new StringColumn("BindingTypeClassName", false);
        public StringColumn BindingConfigurationElementTypeClassName = new StringColumn("BindingConfigurationElementTypeClassName", false);
        public Int32Column ChannelType_id = new Int32Column("ChannelType_id");
    }
}
