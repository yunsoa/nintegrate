﻿using System;
using System.Configuration;
using System.Data.Common;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Data;

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
        /// Create a DbCommand from a query criteria.
        /// </summary>
        /// <param name="criteria">The query criteria.</param>
        /// <param name="isCountCommand">if a count command is expected to create.</param>
        /// <returns></returns>
        public DbCommand CreateCommand(QueryCriteria criteria, bool isCountCommand)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            var connStr = GetConnectionString(criteria.ConnectionStringName);
            if (connStr == null)
                throw new ConfigurationErrorsException(string.Format("Could not find speficied connectionstring key from configuration store: {0}.", criteria.ConnectionStringName));
            var cmdBuilder = GetQueryCommandBuilder(connStr.ProviderName);
            DbCommand cmd;
            if (criteria.QueryType == QueryType.Sproc)
            {
                cmd = cmdBuilder.GetDbProviderFactory().CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = criteria.TableName;
            }
            else
            {
                cmd = cmdBuilder.BuildCommand(criteria, isCountCommand);
            }
            cmd.Connection = cmdBuilder.GetDbProviderFactory().CreateConnection();
            cmd.Connection.ConnectionString = connStr.ConnectionString;

            if (criteria.QueryType == QueryType.Sproc)
            {
                var sprocCmd = new SprocDbCommand(cmd, cmdBuilder);
                foreach (var parameterCondition in criteria.SprocParameterConditions)
                {
                    sprocCmd.AddParameter(parameterCondition);
                }
                return sprocCmd;
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

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        /// <returns></returns>
        protected virtual ConnectionStringSettings GetConnectionString(string connectionStringName)
        {
            return ConfigurationManager.ConnectionStrings[connectionStringName];
        }

        /// <summary>
        /// Gets the query command builder.
        /// </summary>
        /// <param name="providerName">Name of the provider.</param>
        /// <returns></returns>
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
