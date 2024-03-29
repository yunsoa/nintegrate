﻿using System;
using System.Configuration;
using System.Data;
using System.Data.Common;
using NIntegrate.Configuration;

namespace NIntegrate.Query.Command
{
    /// <summary>
    /// The QueryCommandFactory creates query commands by 
    /// calling different query command builders.
    /// </summary>
    public sealed class QueryCommandFactory
    {
        #region Private Fields

        private readonly Criteria _criteria;
        private readonly string _connectionString;
        private readonly IQueryCommandBuilder _queryCommandBuilder;

        #endregion

        #region Private Methods

        private static IQueryCommandBuilder GetQueryCommandBuilder(string providerName)
        {
            if (string.CompareOrdinal(providerName, "System.Data.SqlClient") == 0)
                return SqlClient.SqlQueryCommandBuilder.Instance;
            if (string.CompareOrdinal(providerName, "System.Data.OracleClient") == 0)
                return OracleClient.OracleQueryCommandBuilder.Instance;

            var providerTypeName = ConfigurationManager.AppSettings[providerName];
            var providerType = Type.GetType(providerTypeName);
            if (providerType != null)
                return (IQueryCommandBuilder)Activator.CreateInstance(providerType);

            throw new ConfigurationErrorsException(string.Format("Specified database provider - {0},{1} could not be loaded!", providerName, providerType));
        }

        private void SetConnection(DbCommand cmd)
        {
            cmd.Connection = _queryCommandBuilder.GetDbProviderFactory().CreateConnection();
            cmd.Connection.ConnectionString = _connectionString;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryCommandFactory"/> class.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        public QueryCommandFactory(Criteria criteria)
        {
            _criteria = criteria;

            var connString = ConnectionStringStore.GetConnectionString(criteria.ConnectionStringName);
            if (connString == null)
                throw new ConfigurationErrorsException(string.Format("Specified ConnectionStringName - {0} could not be found in configuration store!", _criteria.ConnectionStringName));
            _queryCommandBuilder = GetQueryCommandBuilder(connString.ProviderName);
            _connectionString = connString.Value;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the query command.
        /// </summary>
        /// <returns></returns>
        public DbCommand GetQueryCommand()
        {
            var cmd = _queryCommandBuilder.BuildQueryCommand(_criteria);
            SetConnection(cmd);
            return cmd;
        }

        /// <summary>
        /// Gets the count command.
        /// </summary>
        /// <returns></returns>
        public DbCommand GetCountCommand()
        {
            var cmd = _queryCommandBuilder.BuildCountCommand(_criteria);
            SetConnection(cmd);
            return cmd;
        }

        /// <summary>
        /// Gets the query data adapter.
        /// </summary>
        /// <returns></returns>
        public DbDataAdapter GetQueryDataAdapter()
        {
            var adapter = _queryCommandBuilder.GetDbProviderFactory().CreateDataAdapter();
            adapter.SelectCommand = GetQueryCommand();

            return adapter;
        }

        /// <summary>
        /// Gets the updatable query data adapter.
        /// </summary>
        /// <param name="option">The option.</param>
        /// <returns></returns>
        public DbDataAdapter GetUpdatableQueryDataAdapter(ConflictOption option)
        {
            var adapter = _queryCommandBuilder.GetDbProviderFactory().CreateDataAdapter();
            adapter.SelectCommand = GetQueryCommand();

            var cmdBuilder = _queryCommandBuilder.GetDbProviderFactory().CreateCommandBuilder();
            cmdBuilder.DataAdapter = adapter;
            cmdBuilder.ConflictOption = option;
            cmdBuilder.GetInsertCommand();
            cmdBuilder.GetUpdateCommand();
            cmdBuilder.GetDeleteCommand();

            return adapter;
        }

        #endregion
    }
}
