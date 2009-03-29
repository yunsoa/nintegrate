using System;
using System.Data;
using NIntegrate.Query.Command;
using NIntegrate.Query;

namespace NIntegrate.Web.UI
{
    public sealed class QueryService : IQueryService
    {
        public DataTable Select(Criteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            using (var adapter = new QueryCommandFactory(criteria).GetQueryDataAdapter())
            {
                var connection = adapter.SelectCommand.Connection;
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    var table = new DataTable(criteria._tableName);
                    adapter.Fill(table);
                    return table;
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                    connection.Dispose();
                }
            }
        }

        public int SelectCount(Criteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            using (var cmd = new QueryCommandFactory(criteria).GetCountCommand())
            {
                var connection = cmd.Connection;
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                    connection.Dispose();
                }
            }
        }

        public int Update(Criteria criteria, DataTable modifiedTable, ConflictOption conflictDetection)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            //var option = (ConflictDetection == System.Web.UI.ConflictOptions.OverwriteChanges ?
            //    ConflictOption.OverwriteChanges : ConflictOption.CompareAllSearchableValues);
            using (var adapter = new QueryCommandFactory(criteria).GetUpdatableQueryDataAdapter(conflictDetection))
            {
                var connection = adapter.SelectCommand.Connection;
                try
                {
                    if (connection.State != ConnectionState.Open)
                        connection.Open();
                    return adapter.Update(modifiedTable);
                }
                finally
                {
                    if (connection.State != ConnectionState.Closed)
                        connection.Close();
                    connection.Dispose();
                }
            }
        }
    }
}
