using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace NIntegrate.Configuration
{
    /// <summary>
    /// The build-in IServiceDeploymentConfigurationProvider implementation.
    /// </summary>
    public class DefaultServiceDeploymentConfigurationProvider 
        : IServiceDeploymentConfigurationProvider
    {
        #region IServiceDeploymentConfigurationProvider Members

        /// <summary>
        /// Gets the service deployment configuration.
        /// </summary>
        /// <param name="appCode">The app code.</param>
        /// <returns></returns>
        public IList<ServiceDeploymentConfiguration> GetServiceDeploymentConfiguration(string appCode)
        {
            var list = new List<ServiceDeploymentConfiguration>();

            using (var conn = new SqlConnection(WcfServiceHelper.GetConfigurationConnectionString()))
            {
                conn.Open();

                var context = new StoredProceduresDataContext(conn);
                var results = context.sp_GetServiceDeploymentConfiguration(
                    Environment.MachineName.ToLowerInvariant(), appCode);

                foreach (var result in results)
                {
                    var serviceDeployment = new ServiceDeploymentConfiguration
                                                {
                                                    ServiceName = result.ServiceName,
                                                    HostXML = result.HostXML,
                                                    EndpointAddress = result.EndpointAddress,
                                                    ListenUri = result.ListenUri
                                                };

                    list.Add(serviceDeployment);
                }

                conn.Close();
                conn.Dispose();
            }

            return list;
        }

        #endregion
    }
}
