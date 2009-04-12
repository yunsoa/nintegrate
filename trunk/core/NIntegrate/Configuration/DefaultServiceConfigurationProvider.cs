﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace NIntegrate.Configuration
{
    public class DefaultServiceConfigurationProvider : IServiceConfigurationProvider
    {
        #region IServiceConfigurationProvider Members

        public IList<BindingType> GetBindingTypes()
        {
            var list = new List<BindingType>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[Constants.ConfigurationDatabaseConnectionStringName].ConnectionString))
            {
                conn.Open();

                var context = new StoredProceduresDataContext(conn);
                var results = context.sp_GetAllBindingTypes();

                foreach (var result in results)
                {
                    var item = new BindingType
                                   {
                                       ChannelType = ((ChannelType) result.ChannelType_id),
                                       ClassName = result.BindingTypeClassName,
                                       ConfigurationElementTypeClassName =
                                           result.BindingConfigurationElementTypeClassName,
                                       FriendlyName = result.BindingTypeFriendlyName,
                                       Type_id = result.BindingType_id
                                   };
                    list.Add(item);
                }

                conn.Close();
                conn.Dispose();
            }

            return list;
        }

        public IList<CustomBehaviorType> GetCustomBehaviorTypes()
        {
            var list = new List<CustomBehaviorType>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[Constants.ConfigurationDatabaseConnectionStringName].ConnectionString))
            {
                conn.Open();

                var context = new StoredProceduresDataContext(conn);
                var results = context.sp_GetAllCustomBehaviorTypes();

                foreach (var result in results)
                {
                    var item = new CustomBehaviorType
                    {
                        ExtensionName = result.BehaviorTypeExtensionName,
                        ClassName = result.BehaviorTypeClassName,
                        ConfigurationElementTypeClassName =
                            result.BehaviorConfigurationElementTypeClassName,
                        FriendlyName = result.BehaviorTypeFriendlyName,
                        Type_id = result.BehaviorType_id
                    };
                    list.Add(item);
                }

                conn.Close();
                conn.Dispose();
            }

            return list;
        }

        public IList<ServiceHostType> GetServiceHostTypes()
        {
            var list = new List<ServiceHostType>();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[Constants.ConfigurationDatabaseConnectionStringName].ConnectionString))
            {
                conn.Open();

                var context = new StoredProceduresDataContext(conn);
                var results = context.sp_GetAllServiceHostTypes();

                foreach (var result in results)
                {
                    var item = new ServiceHostType
                    {
                        ClassName = result.ServiceHostTypeClassName,
                        FriendlyName = result.ServiceHostTypeFriendlyName,
                        Type_id = result.ServiceHostType_id
                    };
                    list.Add(item);
                }

                conn.Close();
                conn.Dispose();
            }

            return list;
        }

        public ServiceConfiguration GetServiceConfiguration(string serviceName, string appCode)
        {
            var config = new ServiceConfiguration();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[Constants.ConfigurationDatabaseConnectionStringName].ConnectionString))
            {
                conn.Open();

                var context = new StoredProceduresDataContext(conn);
                var results = context.sp_GetServiceConfiguration(serviceName, Environment.MachineName.ToLowerInvariant(), appCode);

                var en = results.GetResult<ServiceConfiguration>().GetEnumerator();
                if (en.MoveNext())
                {
                    config.ServiceBehaviorXML = en.Current.ServiceBehaviorXML;
                    config.ServiceHostType_id = en.Current.ServiceHostType_id;
                    config.HostXML = en.Current.HostXML;
                }
                var endpoints = results.GetResult<EndpointConfiguration>();
                config.Endpoints = new List<EndpointConfiguration>(endpoints);

                conn.Close();
                conn.Dispose();
            }

            return config;
        }

        public ClientConfiguration GetClientConfiguration(Type serviceContract, string appCode)
        {
            var config = new ClientConfiguration();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings[Constants.ConfigurationDatabaseConnectionStringName].ConnectionString))
            {
                conn.Open();

                var context = new StoredProceduresDataContext(conn);
                var results = context.sp_GetClientConfiguration(serviceContract.AssemblyQualifiedName, 
                    Environment.MachineName.ToLowerInvariant(), appCode);

                foreach (var result in results)
                {
                    config.Endpoint = new EndpointConfiguration
                                          {
                                              BindingNamespace = result.BindingNamespace,
                                              EndpointAddress = result.EndpointAddress,
                                              EndpointBehaviorXML = result.EndpointBehaviorXML,
                                              IdentityXML = result.IdentityXML,
                                              BindingType_id = result.BindingType_id,
                                              BindingXML = result.BindingXML
                                          };
                    break;
                }

                conn.Close();
                conn.Dispose();
            }

            return config;
        }

        #endregion
    }
}
