using System;
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

        /// <summary>
        /// Create a DbCommand from query criteria.
        /// </summary>
        /// <param name="criteria">The query criteria.</param>
        /// <param name="isCountCommand">if a count command is expected to create.</param>
        /// <returns></returns>
        public DbCommand CreateCommand(QueryCriteria criteria, bool isCountCommand)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            var connStr = GetConnectionString(criteria.ConnectionStringName);
            var cmdBuilder = GetQueryCommandBuilder(connStr.ProviderName);
            var cmd = cmdBuilder.BuildCommand(criteria, isCountCommand);

            if (cmd != null)
            {
                cmd.Connection = cmdBuilder.GetDbProviderFactory().CreateConnection();
                cmd.Connection.ConnectionString = connStr.ConnectionString;
            }

            return cmd;
        }

        /// <summary>
        /// Get DbProviderFactory from query criteria.
        /// </summary>
        /// <param name="criteria">The query criteria.</param>
        /// <returns></returns>
        public DbProviderFactory GetDbProviderFactory(QueryCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            var connStr = GetConnectionString(criteria.ConnectionStringName);
            var cmdBuilder = GetQueryCommandBuilder(connStr.ProviderName);
            return cmdBuilder.GetDbProviderFactory();
        }

        #endregion

        #region Non-Public Methods

        protected virtual ConnectionStringSettings GetConnectionString(string connectionStringName)
        {
            return ConfigurationManager.ConnectionStrings[connectionStringName];
        }

        protected virtual QueryCommandBuilder GetQueryCommandBuilder(string providerName)
        {
            if (string.IsNullOrEmpty(providerName) || string.CompareOrdinal(providerName, "System.Data.SqlClient") == 0)
                return SqlClient.SqlQueryCommandBuilder.Instance;
            if (string.CompareOrdinal(providerName, "System.Data.OracleClient") == 0)
                return OracleClient.OracleQueryCommandBuilder.Instance;

            throw new ConfigurationErrorsException(string.Format(CultureInfo.InvariantCulture, "Specified database provider - {0} could not be loaded!", providerName));
        }

        #endregion
    }
}
