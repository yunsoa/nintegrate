using System;
using System.Data;
using System.Globalization;
using NIntegrate.Data;

namespace NIntegrate.Web
{
    /// <summary>
    /// The default implementation for IQueryService.
    /// </summary>
    public sealed class QueryService : IQueryService
    {
        private readonly QueryCommandFactory _cmdFac;

        public QueryService()
        {
            _cmdFac = new QueryCommandFactory();
        }

        public QueryService(QueryCommandFactory cmdFac)
        {
            if (cmdFac == null)
                throw new ArgumentNullException("cmdFac");

            _cmdFac = cmdFac;
        }

        #region IQueryService Members

        public DataTable Query(QueryCriteria criteria)
        {
            var cmd = _cmdFac.CreateCommand(criteria, false);
            using (var conn = cmd.Connection)
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                var result = new DataTable(criteria.TableName) {Locale = CultureInfo.InvariantCulture};
                var adapter = _cmdFac.GetDbProviderFactory(criteria).CreateDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(result);
                return result;
            }
        }

        public int Execute(QueryCriteria criteria, bool isCountQuery)
        {
            var cmd = _cmdFac.CreateCommand(criteria, isCountQuery);
            using (var conn = cmd.Connection)
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                return isCountQuery ? Convert.ToInt32(cmd.ExecuteScalar()) : cmd.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
