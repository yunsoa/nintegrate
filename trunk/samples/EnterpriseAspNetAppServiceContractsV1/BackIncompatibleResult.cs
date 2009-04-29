using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace EnterpriseAspNetAppServiceContractsV1
{
    [DataContract]
    [ComVisible(true)]
    [Guid("AD42CD75-4223-4b98-B6BB-577EA24A9326")]
    public class BackIncompatibleResult
    {
        [DataMember]
        public string Value { get; set; }
    }
}
