using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace EnterpriseAspNetAppServiceContractsV2
{
    [DataContract]
    [ComVisible(true)]
    [Guid("3D382C14-956D-4c8d-A447-26696307B84B")]
    public class BackCompatibleResultV2
    {
        [DataMember]
        public string Value { get; set; }
        [DataMember]
        public int Value2 { get; set; }
    }
}
