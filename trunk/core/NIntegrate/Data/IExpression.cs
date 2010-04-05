using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace NIntegrate.Data
{
    /// <summary>
    /// An abstract query expression
    /// </summary>
    public interface IExpression : ICloneable
    {
        /// <summary>
        /// Gets the SQL.
        /// </summary>
        /// <value>The SQL.</value>
        [ComVisible(false)]
        string Sql { get; }

        /// <summary>
        /// Gets the child expressions.
        /// </summary>
        /// <value>The child expressions.</value>
        [ComVisible(false)]
        ICollection<IExpression> ChildExpressions { get; }

        /// <summary>
        /// Count
        /// </summary>
        /// <param name="distinct">if distinct</param>
        /// <returns></returns>
        Int32Expression Count(bool distinct);

        /// <summary>
        /// In the specified values.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        Condition In(IEnumerable values);
        /// <summary>
        /// Not in.
        /// </summary>
        /// <param name="values">The values.</param>
        /// <returns></returns>
        Condition NotIn(IEnumerable values);
    }

    /// <summary>
    /// An abstract query parameter expression
    /// </summary>
    public interface IParameterExpression : IExpression
    {
        /// <summary>
        /// Gets the ID.
        /// </summary>
        /// <value>The ID.</value>
        string ID { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [ComVisible(false)]
        object Value { get; set; }

        /// <summary>
        /// Gets the direction.
        /// </summary>
        /// <value>The direction.</value>
        SprocParameterDirection? Direction { get;  }
    }
}
