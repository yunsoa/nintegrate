using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace EnterpriseAspNetAppServiceContractsV2
{
    [DataContract]
    [ComVisible(true)]
    [Guid("4E1CD30D-3B8F-41a6-93B5-9BFDC41F4E96")]
    public class BackIncompatibleResultV2
    {
        [DataMember]
        public int Value { get; set; }
    }
}
