﻿using System.Runtime.Serialization;

namespace NBear.Query
{
    [DataContract]
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
        Modulo,
        [EnumMember]
        BitwiseAnd,
        [EnumMember]
        BitwiseOr,
        [EnumMember]
        BitwiseXOr,
        [EnumMember]
        BitwiseNot
    }
}
