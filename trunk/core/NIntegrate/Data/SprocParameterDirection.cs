using System.Runtime.Serialization;

namespace NIntegrate.Data
{
    [DataContract(Namespace = "http://nintegrate.com")]
    public enum SprocParameterDirection
    {
        [EnumMember]
        Input,
        [EnumMember]
        InputOutput,
        [EnumMember]
        Output,
        [EnumMember]
        ReturnValue
    }
}
