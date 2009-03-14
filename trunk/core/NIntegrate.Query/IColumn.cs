namespace NBear.Query
{
    public interface IColumn : IExpression
    {
        string ColumnName { get; }
    }
}
