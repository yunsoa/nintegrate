using System;
using NIntegrate.ServiceModel.Configuration;

namespace NIntegrate.ServiceModel.Activation
{
    public class LoadServiceConfigurationEventArgs : EventArgs
    {
        public Type ServiceType { get; internal set; }
        public WcfService Service { get; set; }
    }
}
