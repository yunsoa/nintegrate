using System.Collections.Generic;
using System.ServiceModel.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Test.TestClasses;
using NIntegrate.Configuration;
using System.ServiceModel;

namespace NIntegrate.Test
{
    /// <summary>
    ///This is a test class for WcfServiceHostFactoryTest and is intended
    ///to contain all WcfServiceHostFactoryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class WcfServiceHostFactoryTest
    {
        [TestMethod]
        public void TestBindingTypes()
        {
            var basicHttpBinding = ServiceConfigurationStore.GetBindingType(1);
            Assert.IsNotNull(basicHttpBinding);
            Assert.AreEqual(ChannelType.HTTP, basicHttpBinding.ChannelType);
            Assert.AreEqual(typeof(BasicHttpBinding).AssemblyQualifiedName, basicHttpBinding.ClassName);
            Assert.AreEqual(typeof(BasicHttpBindingElement).AssemblyQualifiedName, basicHttpBinding.ConfigurationElementTypeClassName);
            Assert.AreEqual("BasicHttpBinding", basicHttpBinding.FriendlyName);
        }

        [TestMethod]
        public void TestCustomBehaviorTypes()
        {
            var test = ServiceConfigurationStore.GetCustomBehaviorType("test");
            Assert.IsNotNull(test);
            Assert.AreEqual("testclassname", test.ClassName);
            Assert.AreEqual("testelementname", test.ConfigurationElementTypeClassName);
            Assert.AreEqual("testfriendlyname", test.FriendlyName);
        }

        [TestMethod]
        public void TestServiceHostTypes()
        {
            var serviceHost = ServiceConfigurationStore.GetServiceHostType(1);
            Assert.IsNotNull(serviceHost);
            Assert.AreEqual(typeof(ServiceHost).AssemblyQualifiedName, serviceHost.ClassName);
            Assert.AreEqual("ServiceHost", serviceHost.FriendlyName);
        }

        [TestMethod]
        public void TestGetServiceConfiguration()
        {
            var config = ServiceConfigurationStore.GetServiceConfiguration(typeof(TestServiceImpl).AssemblyQualifiedName);
            Assert.IsNotNull(config);
            Assert.AreEqual(1, config.ServiceHostType_id);
            Assert.IsNotNull(config.ServiceBehaviorXML);
            Assert.IsNull(config.HostXML);
            Assert.IsNotNull(config.Endpoints);
            Assert.AreEqual(2, config.Endpoints.Count);
            Assert.IsNull(config.Endpoints[0].BindingNamespace);
            Assert.AreEqual("http://localhost/TestWSHttp", config.Endpoints[0].EndpointAddress);
            Assert.IsNull(config.Endpoints[0].EndpointBehaviorXML);
            Assert.IsNull(config.Endpoints[0].IdentityXML);
            Assert.IsNotNull(config.Endpoints[0].ListenUri);
            Assert.IsNull(config.Endpoints[0].ListenUriMode);
            Assert.AreEqual("WSHttpBinding", ServiceConfigurationStore.GetBindingType(config.Endpoints[0].BindingType_id).FriendlyName);
            Assert.IsNotNull(config.Endpoints[0].BindingXML);
            Assert.IsTrue(config.Endpoints[0].MexBindingEnabled);
        }

        /// <summary>
        ///A test for CreateServiceHost
        ///</summary>
        [TestMethod()]
        public void TestCreateServiceHost()
        {
            var target = new WcfServiceHostFactory();
            var serviceType = typeof(TestServiceImpl);
            var host = target.CreateServiceHost(serviceType);
            host.Open();
            host.Close();
        }
    }
}
