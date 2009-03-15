namespace NIntegrate.Query
{
    public interface IColumn : IExpression
    {
        string ColumnName { get; }
    }
}
