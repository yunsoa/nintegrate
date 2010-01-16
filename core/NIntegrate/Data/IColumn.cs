namespace NIntegrate.Data
{
    public interface IColumn : IExpression
    {
        string ColumnName { get; }
    }
}
