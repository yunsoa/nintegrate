using System;
using System.Collections.Generic;

namespace NIntegrate.Configuration
{
    public class DefaultServiceConfigurationProvider : IServiceConfigurationProvider
    {
        #region IServiceConfigurationProvider Members

        public IList<BindingType> GetBindingTypes()
        {
            throw new NotImplementedException();
        }

        public IList<CustomBehaviorType> GetCustomBehaviorTypes()
        {
            throw new NotImplementedException();
        }

        public IList<ServiceHostType> GetServiceHostTypes()
        {
            throw new NotImplementedException();
        }

        public ServiceConfiguration GetServiceConfiguration(Type serviceContract)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
