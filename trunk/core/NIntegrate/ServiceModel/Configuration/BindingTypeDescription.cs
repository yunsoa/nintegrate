using System;
using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    [DataContract]
    public sealed class BindingTypeDescription
    {
        [DataMember]
        public Type BindingType { get; set; }

        [DataMember]
        public Type BindingConfigurationElementType { get; set; }

        [DataMember]
        public string[] AvailableProtocols { get; set; }
    }
}