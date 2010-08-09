using NIntegrate.Data;
using NIntegrate.ServiceModel.Activation;
using NIntegrate.Test.Query.TestClasses;

namespace NIntegrate.WebTest.Code
{
    public class FarmConnectionServiceHostFactory : WcfServiceHostFactory
    {
        private static ActiveRecordConnection<Farm> _connection = new ActiveRecordConnection<Farm>(new QueryCommandFactory());

        protected override object GetServiceInstance(System.Type serviceType)
        {
            if (serviceType == typeof(IActiveRecordConnection<Farm>))
                return _connection;

            return null;
        }

        public override NIntegrate.ServiceModel.Configuration.WcfService LoadServiceConfiguration(System.Type serviceType)
        {
            if (serviceType == typeof(IActiveRecordConnection<Farm>))
            {
                return new NIntegrate.ServiceModel.Configuration.WcfService
                {
                    ServiceType = _connection.GetType().FullName,
                    ServiceBehaviorXml = new NIntegrate.ServiceModel.Configuration.ServiceBehaviorXml("<behavior name=\"MyServiceTypeBehaviors\"><serviceMetadata httpGetEnabled=\"true\" /></behavior>"),
                    Endpoints = new []
                    {
                        new NIntegrate.ServiceModel.Configuration.WcfServiceEndpoint
                        {
                            ServiceContractType = serviceType.FullName,
                            BindingXml = new NIntegrate.ServiceModel.Configuration.BindingXml("basichttpbinding", null)
                        }
                    }
                };
            }

            return null;
        }
    }
}