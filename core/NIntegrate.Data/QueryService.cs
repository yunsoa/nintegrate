using System.Data;
using System.Globalization;

namespace NIntegrate.Data
{
    public sealed class QueryService : IQueryService
    {
        #region IQueryService Members

        public DataTable Query(QueryCriteria criteria)
        {
            var fac = new QueryCommandFactory();
            var cmd = fac.CreateCommand(criteria, false);
            using (var conn = cmd.Connection)
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                var result = new DataTable(criteria.TableName);
                result.Locale = CultureInfo.InvariantCulture;
                var adapter = fac.GetDbProviderFactory(criteria).CreateDataAdapter();
                adapter.SelectCommand = cmd;
                adapter.Fill(result);
                return result;
            }
        }

        public int Execute(QueryCriteria criteria, bool isCountQuery)
        {
            var fac = new QueryCommandFactory();
            var cmd = fac.CreateCommand(criteria, false);
            using (var conn = cmd.Connection)
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();
                return cmd.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
