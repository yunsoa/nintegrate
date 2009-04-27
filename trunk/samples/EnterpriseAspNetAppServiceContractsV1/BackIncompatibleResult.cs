using System.Runtime.Serialization;

namespace EnterpriseAspNetAppServiceContractsV1
{
    [DataContract]
    public class BackIncompatibleResult
    {
        [DataMember]
        public string Value { get; set; }
    }
}
