﻿using System.Data.Common;

namespace NIntegrate.Query.Command
{
    public interface IQueryCommandBuilder
    {
        string ToParameterName(string name); 
        string GetDatabaseObjectNameQuoteCharacters();
        DbProviderFactory GetDbProviderFactory();
        DbCommand BuildQueryCommand(Criteria criteria);
        DbCommand BuildCountCommand(Criteria criteria);
    }
}