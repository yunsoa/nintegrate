using System.Configuration;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Globalization;

namespace NIntegrate.Data
{
    /// <summary>
    /// The QueryCommandFactory creates query commands by 
    /// calling different query command builders.
    /// </summary>
    [ComVisible(false)]
    public class QueryCommandFactory
    {
        #region Public Methods

        public DbCommand CreateCommand(QueryCriteria criteria)
        {
            if (criteria == null)
                return null;

            var connStr = GetConnectionString(criteria.ConnectionStringName);
            var cmdBuilder = GetQueryCommandBuilder(connStr.ProviderName);
            var cmd = cmdBuilder.BuildCommand(criteria, false);

            if (cmd != null)
            {
                cmd.Connection = cmdBuilder.GetDbProviderFactory().CreateConnection();
                cmd.Connection.ConnectionString = connStr.ConnectionString;
            }

            return cmd;
        }

        public DbCommand CreateCountCommand(QueryCriteria criteria)
        {
            if (criteria == null)
                return null;

            var connStr = GetConnectionString(criteria.ConnectionStringName);
            var cmdBuilder = GetQueryCommandBuilder(connStr.ProviderName);
            var cmd = cmdBuilder.BuildCommand(criteria, true);

            if (cmd != null)
            {
                cmd.Connection = cmdBuilder.GetDbProviderFactory().CreateConnection();
                cmd.Connection.ConnectionString = connStr.ConnectionString;
            }

            return cmd;
        }

        #endregion

        #region Non-Public Methods

        protected virtual ConnectionStringSettings GetConnectionString(string connectionStringName)
        {
            return ConfigurationManager.ConnectionStrings[connectionStringName];
        }

        protected virtual QueryCommandBuilder GetQueryCommandBuilder(string providerName)
        {
            if (string.CompareOrdinal(providerName, "System.Data.SqlClient") == 0)
                return SqlClient.SqlQueryCommandBuilder.Instance;
            if (string.CompareOrdinal(providerName, "System.Data.OracleClient") == 0)
                return OracleClient.OracleQueryCommandBuilder.Instance;

            throw new ConfigurationErrorsException(string.Format(CultureInfo.InvariantCulture, "Specified database provider - {0} could not be loaded!", providerName));
        }

        #endregion
    }
}
