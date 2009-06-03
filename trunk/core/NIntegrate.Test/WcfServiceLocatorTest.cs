using System.ServiceModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Configuration;
using NIntegrate.Test.TestClasses;

namespace NIntegrate.Test
{
    /// <summary>
    ///This is a test class for WcfServiceLocatorTest and is intended
    ///to contain all WcfServiceLocatorTest Unit Tests
    ///</summary>
    [TestClass]
    public class WcfServiceLocatorTest
    {
        [TestMethod]
        public void TestGetClientConfiguration()
        {
            var config = ServiceConfigurationStore.GetClientConfiguration(typeof(ITestService));
            Assert.IsNotNull(config);
            Assert.IsNotNull(config.Endpoint);
            Assert.IsNull(config.Endpoint.BindingNamespace);
            Assert.AreEqual("net.tcp://localhost:809/TestNetTcp", config.Endpoint.EndpointAddress);
            Assert.IsNull(config.Endpoint.EndpointBehaviorXML);
            Assert.IsNull(config.Endpoint.IdentityXML);
            Assert.IsNotNull(config.Endpoint.ListenUri);
            Assert.IsNull(config.Endpoint.ListenUriMode);
            Assert.AreEqual("NetTcpBinding", ServiceConfigurationStore.GetBindingType(config.Endpoint.BindingType_id).FriendlyName);
            Assert.IsNull(config.Endpoint.BindingXML);
            Assert.IsFalse(config.Endpoint.MexBindingEnabled);
        }

        /// <summary>
        ///A test for GetService
        ///</summary>
        [TestMethod]
        public void TestGetService()
        {
            var target = new WcfServiceLocator();
            var serviceContract = typeof(ITestService);
            var service = (ITestService)target.GetService(serviceContract);
            try
            {
                service.NormalOperation("test");
            }
            catch (EndpointNotFoundException)
            {
                //ok
            }
            target.Dispose();
        }
    }
}
