﻿using System.Runtime.Serialization;

namespace NIntegrate.Data
{
    [DataContract(Namespace = "http://nintegrate.com")]
    public enum ExpressionOperator
    {
        [EnumMember]
        None,

        [EnumMember]
        Equals,

        [EnumMember]
        NotEquals,

        [EnumMember]
        In,

        [EnumMember]
        GreaterThan,

        [EnumMember]
        GreaterThanOrEquals,

        [EnumMember]
        LessThan,

        [EnumMember]
        LessThanOrEquals,

        [EnumMember]
        Like,

        [EnumMember]
        Is,

        [EnumMember]
        IsNot,

        [EnumMember]
        Add,

        [EnumMember]
        Subtract,

        [EnumMember]
        Multiply,

        [EnumMember]
        Divide,

        [EnumMember]
        Mod,

        [EnumMember]
        BitwiseAnd,

        [EnumMember]
        BitwiseOr,

        [EnumMember]
        BitwiseXor,

        [EnumMember]
        BitwiseNot
    }
}
