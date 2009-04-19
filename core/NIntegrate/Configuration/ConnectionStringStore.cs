using System;
using System.Collections.Generic;
using System.Configuration;

namespace NIntegrate.Configuration
{
    public sealed class ConnectionStringStore
    {
        #region Private Singleton

        private static readonly ConnectionStringStore _singleton = new ConnectionStringStore();

        #endregion

        #region Private Constructor

        private readonly IConnectionStringProvider _provider;

        private ConnectionStringStore()
        {
            var providerTypeName = ConfigurationManager.AppSettings[Constants.ConnectionStringProviderTypeAppSettingName];
            if (!string.IsNullOrEmpty(providerTypeName))
            {
                var providerType = Type.GetType(providerTypeName);
                if (providerType != null)
                    _provider = (IConnectionStringProvider) Activator.CreateInstance(providerType);
            }

            if (_provider == null)
                _provider = new DefaultConnectionStringProvider();
        }

        #endregion

        #region Private Connection String Cache

        private static readonly Dictionary<string, ConnectionString> _cachedConnectionStrings
            = new Dictionary<string, ConnectionString>();

        #endregion

        public static ConnectionString GetConnectionString(string connectionStringName)
        {
            if (string.IsNullOrEmpty(connectionStringName))
                throw new ArgumentNullException("connectionStringName");

            if (!_cachedConnectionStrings.ContainsKey(connectionStringName))
            {
                lock (_cachedConnectionStrings)
                {
                    if (!_cachedConnectionStrings.ContainsKey(connectionStringName))
                    {
                        var connString = _singleton._provider.GetConnectionString(connectionStringName);
                        if (connString == null)
                            throw new ConfigurationErrorsException(string.Format("Specified ConnectionStringName - {0} could not be found in configuration store!", connectionStringName));
                        _cachedConnectionStrings.Add(connectionStringName, connString);
                    }
                }
            }

            return _cachedConnectionStrings[connectionStringName];
        }

        public static void ClearCache()
        {
            lock (_cachedConnectionStrings)
            {
                _cachedConnectionStrings.Clear();
            }
        }
    }
}
