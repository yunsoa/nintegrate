using System.Runtime.Serialization;

namespace EnterpriseAspNetAppServiceContractsV1
{
    [DataContract]
    public class BackCompatibleResult
    {
        [DataMember]
        public string Value { get; set; }
    }
}
