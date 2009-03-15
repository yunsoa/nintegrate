using System.Data.Common;

namespace NBear.Query
{
    public interface IQueryCommandBuilder
    {
        string ToParameterName(string name); 
        string GetTableOrColumnNameQuoteCharactors();
        DbProviderFactory GetDbProviderFactory();
        DbCommand BuildQueryCommand(Criteria criteria);
        DbCommand BuildCountCommand(Criteria criteria);
    }
}
