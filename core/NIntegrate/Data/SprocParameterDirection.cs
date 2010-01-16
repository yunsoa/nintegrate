using System.Runtime.Serialization;

namespace NIntegrate.Data
{
    [DataContract]
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
