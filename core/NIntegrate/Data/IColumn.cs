namespace NIntegrate.Data
{
    /// <summary>
    /// The interface represnts an abstract query column expression
    /// </summary>
    public interface IColumn : IExpression
    {
        /// <summary>
        /// Gets the name of the column.
        /// </summary>
        /// <value>The name of the column.</value>
        string ColumnName { get; }
    }
}
