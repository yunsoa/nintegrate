using System.Collections.Generic;

namespace NIntegrate.Configuration
{
    public sealed class ServiceConfiguration
    {
        public string ServiceName { get; set; }
        public string ServiceBehaviorXML { get; set; }
        public int ServiceHostType_id { get; set; }
        public string HostXML { get; set; }
        public string ClientMetadataXML { get; set; }

        public IList<EndpointConfiguration> Endpoints { get; set; }
    }
}
