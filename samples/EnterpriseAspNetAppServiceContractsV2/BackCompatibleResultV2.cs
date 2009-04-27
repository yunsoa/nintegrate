using System.Runtime.Serialization;

namespace EnterpriseAspNetAppServiceContractsV2
{
    [DataContract]
    public class BackCompatibleResultV2
    {
        public string Value { get; set; }
        public int Value2 { get; set; }
    }
}
