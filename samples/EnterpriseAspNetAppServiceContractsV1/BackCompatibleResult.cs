using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace EnterpriseAspNetAppServiceContractsV1
{
    [DataContract]
    [ComVisible(true)]
    [Guid("2F9A8109-0B10-4d95-B9D6-C6EC4AD6D0C2")]
    public class BackCompatibleResult
    {
        [DataMember]
        public string Value { get; set; }
    }
}
