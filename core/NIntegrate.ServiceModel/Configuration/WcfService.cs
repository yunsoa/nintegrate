using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    [DataContract]
    public sealed class WcfService
    {
        [DataMember]
        public string CustomServiceHostType { get; set; }

        [DataMember]
        public ServiceBehaviorXml ServiceBehaviorXml { get; set; }

        [DataMember]
        public HostXml HostXml { get; set; }

        [DataMember]
        public WcfServiceEndpoint[] Endpoints { get; set; }
    }
}
