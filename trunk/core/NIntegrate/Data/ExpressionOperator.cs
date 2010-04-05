using System.Runtime.Serialization;

namespace NIntegrate.Data
{
    /// <summary>
    /// Query expression operators
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public enum ExpressionOperator
    {
        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        None,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        Equals,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        NotEquals,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        In,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        GreaterThan,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        GreaterThanOrEquals,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        LessThan,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        LessThanOrEquals,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        Like,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        Is,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        IsNot,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        Add,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        Subtract,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        Multiply,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        Divide,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        Mod,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        BitwiseAnd,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        BitwiseOr,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        BitwiseXor,

        /// <summary>
        /// 
        /// </summary>
        [EnumMember]
        BitwiseNot
    }
}
