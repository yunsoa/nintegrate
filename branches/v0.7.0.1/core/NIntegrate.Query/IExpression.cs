using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NIntegrate.Query
{
    public interface IExpression : ICloneable
    {
        [ComVisible(false)]
        string Sql { get; }
        [ComVisible(false)]
        IList<IExpression> ChildExpressions { get; }

        Int32Expression Count(bool distinct);

        Condition In(IEnumerable values);
        Condition NotIn(IEnumerable values);
    }

    public interface IParameterExpression : IExpression
    {
        string ID { get; }
        [ComVisible(false)]
        object Value { get; set; }
    }
}
