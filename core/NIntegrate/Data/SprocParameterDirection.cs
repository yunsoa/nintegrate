using System.Runtime.Serialization;

namespace NIntegrate.Data
{
    /// <summary>
    /// Direction of a stored procedure parameter
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public enum SprocParameterDirection
    {
        /// <summary>
        /// Input
        /// </summary>
        [EnumMember]
        Input,
        /// <summary>
        /// InputOutput
        /// </summary>
        [EnumMember]
        InputOutput,
        /// <summary>
        /// Output
        /// </summary>
        [EnumMember]
        Output,
        /// <summary>
        /// ReturnValue
        /// </summary>
        [EnumMember]
        ReturnValue
    }
}
