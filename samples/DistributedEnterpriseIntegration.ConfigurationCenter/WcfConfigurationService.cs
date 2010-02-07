using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DistributedEnterpriseIntegration.Framework;
using DistributedEnterpriseIntegration.ConfigurationCenter.Sprocs;
using NIntegrate.Data;
using NIntegrate.Mapping;
using System.Data;
using NIntegrate.ServiceModel.Configuration;

namespace DistributedEnterpriseIntegration.ConfigurationCenter
{
    public class WcfConfigurationService : IWcfConfigurationService
    {
        private static readonly QueryCommandFactory _commandfactory = new QueryCommandFactory();
        private static readonly MapperFactory _mapperFac = new MapperFactory();

        #region Constructors

        static WcfConfigurationService()
        {
            //predefine the custom mapper from IDataReader to WcfEndpoint
            _mapperFac.ConfigureMapper<IDataReader, WcfEndpoint>(
                true, true, false,
                "BindingXml", "BindingTypeCode", "EndpointBehaviorXml", "IdentityXml"
            )
            .From(rdr => new BindingXml(
                rdr.GetString(rdr.GetOrdinal("BindingTypeCode")),
                rdr.IsDBNull(rdr.GetOrdinal("BindingXml")) ? null : rdr.GetString(rdr.GetOrdinal("BindingXml"))
            ))
            .To<BindingXml>((endpoint, val) =>
            {
                endpoint.BindingXml = val;
                return endpoint;
            })
            .From(rdr => rdr.IsDBNull(rdr.GetOrdinal("EndpointBehaviorXml")) ? null : new EndpointBehaviorXml(rdr.GetString(rdr.GetOrdinal("EndpointBehaviorXml"))))
            .To<EndpointBehaviorXml>((endpoint, val) =>
            {
                endpoint.EndpointBehaviorXml = val;
                return endpoint;
            })
            .From(rdr => rdr.IsDBNull(rdr.GetOrdinal("IdentityXml")) ? null : new IdentityXml(rdr.GetString(rdr.GetOrdinal("IdentityXml"))))
            .To<IdentityXml>((endpoint, val) =>
            {
                endpoint.IdentityXml = val;
                return endpoint;
            });
        }

        #endregion

        #region IWcfConfigurationService Members

        public NIntegrate.ServiceModel.Configuration.WcfService GetWcfService(string serviceType, string serverName, string loadBalancePath)
        {
            var getWcfService = new GetWcfService();
            var criteria = getWcfService.CreateSprocCriteria(
                getWcfService.ServiceType == serviceType,
                getWcfService.ServerName == serverName,
                getWcfService.LoadBalancePath == loadBalancePath);
            using (var cmd = _commandfactory.CreateCommand(criteria, false))
            {
                using (var conn = cmd.Connection)
                {
                    conn.Open();
                    var rdr = cmd.ExecuteReader();

                    var service = new WcfService { ServiceType = serviceType };
                    var endpointList = new List<WcfServiceEndpoint>();

                    while (rdr.Read())
                    {
                        //int ordAddress = rdr.GetOrdinal("Address");
                        //int ordBindingXml = rdr.GetOrdinal("BindingXml");
                        //int ordBindingTypeCode = rdr.GetOrdinal("BindingTypeCode");
                        //int ordEndpointBehaviorXml = rdr.GetOrdinal("EndpointBehaviorXml");
                        //int ordIdentityXml = rdr.GetOrdinal("IdentityXml");
                        //int ordServiceContractType = rdr.GetOrdinal("ServiceContractType");

                        //var endpoint = new WcfServiceEndpoint();

                        //endpoint.Address = rdr.GetString(ordAddress);
                        //endpoint.BindingXml = new BindingXml(rdr.GetString(ordBindingTypeCode), rdr.GetString(ordBindingXml));
                        //endpoint.EndpointBehaviorXml = rdr.IsDBNull(ordEndpointBehaviorXml) ? null : new EndpointBehaviorXml(rdr.GetString(ordEndpointBehaviorXml));
                        //endpoint.IdentityXml = rdr.IsDBNull(ordIdentityXml) ? null : new IdentityXml(rdr.GetString(ordIdentityXml));
                        //endpoint.ServiceContractType = rdr.GetString(ordServiceContractType);

                        var dataReaderToEndpointMapper = _mapperFac.GetMapper<IDataReader, WcfEndpoint>();
                        var serviceEndpointMapper = _mapperFac.GetMapper<WcfEndpoint, WcfServiceEndpoint>();
                        var endpoint = serviceEndpointMapper(dataReaderToEndpointMapper(rdr));
                        endpointList.Add(endpoint);
                    }

                    service.Endpoints = endpointList.ToArray();

                    return service;
                }
            }
        }

        public NIntegrate.ServiceModel.Configuration.WcfClientEndpoint GetWcfClientEndpoint(string serviceContractType, string serverName)
        {
            var getWcfClientEndpoint = new GetWcfClientEndpoint();
            var criteria = getWcfClientEndpoint.CreateSprocCriteria(
                getWcfClientEndpoint.ServiceContractType == serviceContractType,
                getWcfClientEndpoint.ServerName == serverName);
            using (var cmd = _commandfactory.CreateCommand(criteria, false))
            {
                using (var conn = cmd.Connection)
                {
                    conn.Open();
                    var rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        //var endpoint = new WcfClientEndpoint();
                        //int ordAddress = rdr.GetOrdinal("Address");
                        //int ordBindingXml = rdr.GetOrdinal("BindingXml");
                        //int ordBindingTypeCode = rdr.GetOrdinal("BindingTypeCode");
                        //int ordEndpointBehaviorXml = rdr.GetOrdinal("EndpointBehaviorXml");
                        //int ordIdentityXml = rdr.GetOrdinal("IdentityXml");
                        //int ordServiceContractType = rdr.GetOrdinal("ServiceContractType");

                        //endpoint.Address = rdr.GetString(ordAddress);
                        //endpoint.BindingXml = new BindingXml(rdr.GetString(ordBindingTypeCode), rdr.GetString(ordBindingXml));
                        //endpoint.EndpointBehaviorXml = rdr.IsDBNull(ordEndpointBehaviorXml) ? null : new EndpointBehaviorXml(rdr.GetString(ordEndpointBehaviorXml));
                        //endpoint.IdentityXml = rdr.IsDBNull(ordIdentityXml) ? null : new IdentityXml(rdr.GetString(ordIdentityXml));
                        //endpoint.ServiceContractType = rdr.GetString(ordServiceContractType);

                        var dataReaderToEndpointMapper = _mapperFac.GetMapper<IDataReader, WcfEndpoint>();
                        var clientEndpointMapper = _mapperFac.GetMapper<WcfEndpoint, WcfClientEndpoint>();
                        var endpoint = clientEndpointMapper(dataReaderToEndpointMapper(rdr));

                        return endpoint;
                    }

                    return null;
                }
            }
        }

        #endregion
    }
}
