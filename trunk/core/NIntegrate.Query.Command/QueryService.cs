using System;
using System.Data;

namespace NIntegrate.Query.Command
{
    /// <summary>
    /// The build-in implementation of IQueryService.
    /// </summary>
    public sealed class QueryService : IQueryService
    {
        /// <summary>
        /// Selects the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Selects the count.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Updates the specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="modifiedTable">The modified table.</param>
        /// <param name="conflictDetection">The conflict detection.</param>
        /// <returns></returns>
        public int Update(Criteria criteria, DataTable modifiedTable, ConflictOption conflictDetection)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

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
