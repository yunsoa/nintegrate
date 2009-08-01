using NIntegrate.Data;
using System.Runtime.Serialization;

namespace NIntegrate.Test.DatabaseScripts.Criterias
{
    [DataContract]
    public sealed class BindingTypeTable : QueryTable
    {
        public BindingTypeTable()
            : base("BindingType_lkp", "NIntegrateConfig", false)
        {
        }

        public Int32Column BindingType_id = new Int32Column("BindingType_id");
        public StringColumn BindingTypeFriendlyName = new StringColumn("BindingTypeFriendlyName", false);
        public StringColumn BindingTypeClassName = new StringColumn("BindingTypeClassName", false);
        public StringColumn BindingConfigurationElementTypeClassName = new StringColumn("BindingConfigurationElementTypeClassName", false);
        public Int32Column ChannelType_id = new Int32Column("ChannelType_id");
    }
}
