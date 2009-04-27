using System.Runtime.Serialization;

namespace EnterpriseAspNetAppServiceContractsV2
{
    [DataContract]
    public class BackIncompatibleResultV2
    {
        [DataMember]
        public int Value { get; set; }
    }
}
