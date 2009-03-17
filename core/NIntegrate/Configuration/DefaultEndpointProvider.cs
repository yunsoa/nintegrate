using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;

namespace NIntegrate.Configuration
{
    public class DefaultEndpointProvider : IEndpointProvider
    {
        #region IEndpointProvider Members

        public IList<Endpoint> GetServerEndpoints(Type serviceContract)
        {
            var list = new List<Endpoint>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[Constants.ConfigurationDatabaseConnectionStringName].ConnectionString))
            {
                conn.Open();

                var context = new StoredProceduresDataContext(conn);
                var results = context.sp_GetServerEndpoints(serviceContract.ToString(),
                    WcfServiceHelper.GetServiceVersion(serviceContract.Assembly), 
                    Environment.MachineName.ToLowerInvariant());

                foreach (var result in results)
                {
                    var endpoint = new Endpoint
                                       {
                                           Address = result.Address,
                                           ChannelType = result.ChannelType,
                                           CloseTimeout = result.CloseTimeout,
                                           FarmAddress = result.FarmAddress,
                                           MexBindingEnabled = result.MexBindingEnabled,
                                           IncludeExceptionDetailInFaults = result.IncludeExceptionDetailInFaults,
                                           ListenBacklog = result.ListenBacklog,
                                           MaxBufferPoolSize = result.MaxBufferPoolSize,
                                           MaxBufferSize = result.MaxBufferSize,
                                           MaxConcurrentCalls = result.MaxConcurrentCalls,
                                           MaxConcurrentInstances = result.MaxConcurrentInstances,
                                           MaxConcurrentSessions = result.MaxConcurrentSessions,
                                           MaxConnections = result.MaxConnections,
                                           MaxReceivedMessageSize = result.MaxReceivedMessageSize,
                                           OpenTimeout = result.OpenTimeout,
                                           PortSharingEnabled = result.PortSharingEnabled,
                                           ReceiveTimeout = result.ReceiveTimeout,
                                           SecurityMode = result.SecurityMode,
                                           ClientCredentialTypeName = result.ClientCredentialTypeName,
                                           SendTimeout = result.SendTimeout,
                                           TransactionFlow = result.TransactionFlow,
                                           TransactionTimeout = result.TransactionTimeout,
                                           TransferMode = result.TransferMode,
                                           ReliableSessionEnabled = result.ReliableSessionEnabled,
                                           ReliableSessionInactivityTimeout = result.ReliableSessionInactivityTimeout,
                                           ReliableSessionOrdered = result.ReliableSessionOrdered
                                       };
                    list.Add(endpoint);
                }

                conn.Close();
                conn.Dispose();
            }

            return list;
        }

        public IList<Endpoint> GetClientEndpoints(Type serviceContract)
        {
            var list = new List<Endpoint>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[Constants.ConfigurationDatabaseConnectionStringName].ConnectionString))
            {
                conn.Open();

                var context = new StoredProceduresDataContext(conn);
                var results = context.sp_GetClientEndpoints(serviceContract.ToString(),
                    WcfServiceHelper.GetServiceVersion(serviceContract.Assembly),
                    Environment.MachineName.ToLowerInvariant());

                foreach (var result in results)
                {
                    var endpoint = new Endpoint
                    {
                        Address = result.Address,
                        ChannelType = result.ChannelType,
                        CloseTimeout = result.CloseTimeout,
                        FarmAddress = result.FarmAddress,
                        MexBindingEnabled = result.MexBindingEnabled,
                        IncludeExceptionDetailInFaults = result.IncludeExceptionDetailInFaults,
                        ListenBacklog = result.ListenBacklog,
                        MaxBufferPoolSize = result.MaxBufferPoolSize,
                        MaxBufferSize = result.MaxBufferSize,
                        MaxConcurrentCalls = result.MaxConcurrentCalls,
                        MaxConcurrentInstances = result.MaxConcurrentInstances,
                        MaxConcurrentSessions = result.MaxConcurrentSessions,
                        MaxConnections = result.MaxConnections,
                        MaxReceivedMessageSize = result.MaxReceivedMessageSize,
                        OpenTimeout = result.OpenTimeout,
                        PortSharingEnabled = result.PortSharingEnabled,
                        ReceiveTimeout = result.ReceiveTimeout,
                        SecurityMode = result.SecurityMode,
                        ClientCredentialTypeName = result.ClientCredentialTypeName,
                        SendTimeout = result.SendTimeout,
                        TransactionFlow = result.TransactionFlow,
                        TransactionTimeout = result.TransactionTimeout,
                        TransferMode = result.TransferMode,
                        ReliableSessionEnabled = result.ReliableSessionEnabled,
                        ReliableSessionInactivityTimeout = result.ReliableSessionInactivityTimeout,
                        ReliableSessionOrdered = result.ReliableSessionOrdered
                    };
                    list.Add(endpoint);
                }

                conn.Close();
                conn.Dispose();
            }

            return list;
        }

        #endregion
    }
}
