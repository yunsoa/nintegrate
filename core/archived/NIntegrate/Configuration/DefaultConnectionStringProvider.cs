using System;
using System.Configuration;
using System.Data.SqlClient;

namespace NIntegrate.Configuration
{
    /// <summary>
    /// The build-in IConnectionStringProvider implementaion.
    /// </summary>
    public class DefaultConnectionStringProvider : IConnectionStringProvider
    {
        #region IConnectionStringProvider Members

        /// <summary>
        /// Get connection string by given connectionStringName.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        /// <returns></returns>
        public ConnectionString GetConnectionString(string connectionStringName)
        {
            if (string.IsNullOrEmpty(connectionStringName))
                throw new ArgumentNullException("connectionStringName");

            ConnectionString connectionString = null;

            if (connectionStringName == Constants.ConfigurationDatabaseConnectionStringName)
            {
                connectionString = new ConnectionString
                                       {
                                           ProviderName = "System.Data.SqlClient",
                                           Value = WcfServiceHelper.GetConfigurationConnectionString()
                                       };
                return connectionString;
            }

            using (var conn = new SqlConnection(WcfServiceHelper.GetConfigurationConnectionString()))
            {
                conn.Open();

                var context = new StoredProceduresDataContext(conn);
                var results = context.sp_GetConnectionString(connectionStringName,
                    Environment.MachineName.ToLowerInvariant());

                foreach (var result in results)
                {
                    connectionString = new ConnectionString {Value = result.Value, ProviderName = result.ProviderName};
                    break;
                }

                conn.Close();
                conn.Dispose();
            }

            if (connectionString == null)
                throw new ConfigurationErrorsException("Specified connection string name '" + connectionStringName + "' doesn't exist for your server farm!");

            return connectionString;
        }

        #endregion
    }
}
