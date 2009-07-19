using System;
using System.ServiceModel;
using NIntegrate.ServiceModel.Configuration;

namespace NIntegrate.ServiceModel
{
    public static class ChannelFactoryFactory
    {
        public static ChannelFactory<TChannel> CreateChannelFactory<TChannel>(WcfClientEndpoint endpoint, params Uri[] baseAddresses)
        {
            if (endpoint == null)
                throw new ArgumentNullException("endpoint");
            if (endpoint.BindingXml == null)
                throw new ArgumentNullException("endpoint.BindingXml");

            var binding = endpoint.BindingXml.CreateBinding();
            var address = endpoint.BindingXml.CreateAddress(endpoint.Address, baseAddresses);
            var endpointAddress =
                new EndpointAddress(new Uri(address),
                                    endpoint.IdentityXml == null
                                        ? null
                                        : endpoint.IdentityXml.CreateEndpointIdentity(),
                                    endpoint.HeadersXml == null
                                        ? null
                                        : endpoint.HeadersXml.CreateAddressHeaders());

            var factory = new ChannelFactory<TChannel>(binding, endpointAddress);
            if (endpoint.EndpointBehaviorXml != null)
                endpoint.EndpointBehaviorXml.ApplyEndpointBehaviorConfiguration(factory.Endpoint);

            return factory;
        }
    }
}
