using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Test.TestClasses;
using NIntegrate.ServiceModel.Configuration;

namespace NIntegrate.Test
{
    /// <summary>
    ///This is a test class for WcfServiceHostFactoryTest and is intended
    ///to contain all WcfServiceHostFactoryTest Unit Tests
    ///</summary>
    [TestClass]
    public class WcfServiceHostFactoryTest
    {
        [ClassInitialize]
        public static void MyClassInitialize(TestContext testContext)
        {
            BehaviorExtensionRegistry.Instance.AddItem("testBehavior", typeof (TestServiceBehaviorElement));
            BindingElementExtensionRegistry.Instance.AddItem("testBindingExtension", typeof (TestBindingExtensionElement));
        }

        [TestMethod]
        public void TestServiceHosting()
        {
            var services = GetTestCases();

            var baseAddresses = new Uri[0];

            foreach (var service in services)
            {
                var serviceHost = ServiceModel.Activation.WcfServiceHostFactory.CreateServiceHost(typeof(TestServiceImpl), ref baseAddresses, null, service);
                serviceHost.Open();
                serviceHost.Close();
            }
        }

        #region Test Cases

        private WcfService CreateService()
        {
            var service = new WcfService
                              {
                                  HostXml =
                                      new HostXml(
                                      "<host><baseAddresses><add baseAddress=\"http://localhost:88/TestService\" /><add baseAddress=\"net.tcp://localhost:888/TestService\" /><add baseAddress=\"net.pipe://localhost/TestService\" /></baseAddresses></host>")
                              };
            return service;
        }

        private List<WcfService> GetTestCases()
        {
            var services = new List<WcfService>();

            services.Add(BasicHttpDefaultAddressTestCase());
            services.Add(BasicHttpFullAddressTestCase());
            services.Add(BasicHttpRelativeAddressTestCase());
            services.Add(BasicHttpAbsoluteAddressTestCase());
            services.Add(BasicHttpAndNetTcpEndpointsTestCase());
            services.Add(BasicHttpAndMexHttpEndpointsTestCase());
            services.Add(NetTcpAndMexHttpEndpointsTestCase());
            services.Add(NetTcpAndMexTcpEndpointsTestCase());
            services.Add(BasicHttpAndNetTcpEndpointsServingDifferentSericeContractsTestCase());
            services.Add(TwoBasicHttpEndpointsServingDifferentSericeContractSharingSameListenUriTestCase());
            services.Add(TwoBasicHttpEndpointsServingSameContractSharingSameListenUriTestCase());
            services.Add(NetNamedPipeAndMexNamedPipeEndpointsTestCase());
            services.Add(CustomEndpointsTestCase());

            return services;
        }

        private WcfService BasicHttpDefaultAddressTestCase()
        {
            var service = CreateService();
            service.ServiceBehaviorXml = new ServiceBehaviorXml(
                "<behavior name=\"TestServiceBehavior\" ><serviceMetadata httpGetEnabled=\"true\" /><testBehavior /></behavior>");

            var endpoint = new WcfServiceEndpoint
            {
                BindingXml = new BindingXml("basicHttpBinding", "<binding name=\"basicHttpBinding\"><readerQuotas maxArrayLength=\"16384\" /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6"
            };
            service.Endpoints = new[] { endpoint };

            return service;
        }

        private WcfService BasicHttpRelativeAddressTestCase()
        {
            var service = CreateService();
            service.ServiceBehaviorXml = new ServiceBehaviorXml(
                "<behavior name=\"TestServiceBehavior\" ><serviceMetadata httpGetEnabled=\"true\" /></behavior>");

            var endpoint = new WcfServiceEndpoint
            {
                Address = "relative",
                BindingXml = new BindingXml("basicHttpBinding", "<binding name=\"basicHttpBinding\"><readerQuotas maxArrayLength=\"16384\" /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6"
            };
            service.Endpoints = new[] { endpoint };

            return service;
        }

        private WcfService BasicHttpFullAddressTestCase()
        {
            var service = CreateService();
            service.ServiceBehaviorXml = new ServiceBehaviorXml(
                "<behavior name=\"TestServiceBehavior\" ><serviceMetadata httpGetEnabled=\"true\" /></behavior>");

            var endpoint = new WcfServiceEndpoint
            {
                Address = "http://localhost:88/123",
                BindingXml = new BindingXml("basicHttpBinding", "<binding name=\"basicHttpBinding\"><readerQuotas maxArrayLength=\"16384\" /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6"
            };
            service.Endpoints = new[] { endpoint };

            return service;
        }

        private WcfService BasicHttpAbsoluteAddressTestCase()
        {
            var service = CreateService();
            service.ServiceBehaviorXml = new ServiceBehaviorXml(
                "<behavior name=\"TestServiceBehavior\" ><serviceMetadata httpGetEnabled=\"true\" /></behavior>");

            var endpoint = new WcfServiceEndpoint
            {
                Address = "/relative",
                BindingXml = new BindingXml("basicHttpBinding", "<binding name=\"basicHttpBinding\"><readerQuotas maxArrayLength=\"16384\" /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6"
            };
            service.Endpoints = new[] { endpoint };

            return service;
        }

        private WcfService BasicHttpAndNetTcpEndpointsTestCase()
        {
            var service = CreateService();
            service.ServiceBehaviorXml = new ServiceBehaviorXml(
                "<behavior name=\"TestServiceBehavior\" ><serviceMetadata httpGetEnabled=\"true\" /></behavior>");

            var endpoint = new WcfServiceEndpoint
            {
                BindingXml = new BindingXml("basicHttpBinding", "<binding name=\"basicHttpBinding\"><readerQuotas maxArrayLength=\"16384\" /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6"
            };

            var endpoint2 = new WcfServiceEndpoint
            {
                BindingXml = new BindingXml("netTcpBinding", "<binding name=\"netTcpBinding\"><readerQuotas maxArrayLength=\"16384\" /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6"
            };

            service.Endpoints = new[] { endpoint, endpoint2 };

            return service;
        }

        private WcfService BasicHttpAndMexHttpEndpointsTestCase()
        {
            var service = CreateService();
            service.ServiceBehaviorXml = new ServiceBehaviorXml(
                "<behavior name=\"TestServiceBehavior\" ><serviceMetadata httpGetEnabled=\"true\" /></behavior>");

            var endpoint = new WcfServiceEndpoint
            {
                BindingXml = new BindingXml("basicHttpBinding", "<binding name=\"basicHttpBinding\"><readerQuotas maxArrayLength=\"16384\" /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6"
            };

            var endpoint2 = new WcfServiceEndpoint
            {
                Address = "mex",
                BindingXml = new BindingXml("mexHttpBinding", null),
                ServiceContractType = "IMetadataExchange"
            };

            service.Endpoints = new[] { endpoint, endpoint2 };

            return service;
        }

        private WcfService NetTcpAndMexHttpEndpointsTestCase()
        {
            var service = CreateService();
            service.ServiceBehaviorXml = new ServiceBehaviorXml(
                "<behavior name=\"TestServiceBehavior\" ><serviceMetadata httpGetEnabled=\"true\" /></behavior>");

            var endpoint = new WcfServiceEndpoint
            {
                BindingXml = new BindingXml("netTcpBinding", "<binding name=\"netTcpBinding\"><readerQuotas maxArrayLength=\"16384\" /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6"
            };

            var endpoint2 = new WcfServiceEndpoint
            {
                Address = "mex",
                BindingXml = new BindingXml("mexHttpBinding", null),
                ServiceContractType = "IMetadataExchange"
            };

            service.Endpoints = new[] { endpoint, endpoint2 };

            return service;
        }

        private WcfService NetTcpAndMexTcpEndpointsTestCase()
        {
            var service = CreateService();
            service.ServiceBehaviorXml = new ServiceBehaviorXml(
                "<behavior name=\"TestServiceBehavior\" ><serviceMetadata httpGetEnabled=\"false\" /></behavior>");

            var endpoint = new WcfServiceEndpoint
            {
                BindingXml = new BindingXml("netTcpBinding", "<binding name=\"netTcpBinding\"><readerQuotas maxArrayLength=\"16384\" /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6"
            };

            var endpoint2 = new WcfServiceEndpoint
            {
                Address = "mextcp",
                BindingXml = new BindingXml("mexTcpBinding", null),
                ServiceContractType = "IMetadataExchange"
            };

            service.Endpoints = new[] { endpoint, endpoint2 };

            return service;
        }

        private WcfService BasicHttpAndNetTcpEndpointsServingDifferentSericeContractsTestCase()
        {
            var service = CreateService();
            service.ServiceBehaviorXml = new ServiceBehaviorXml(
                "<behavior name=\"TestServiceBehavior\" ><serviceMetadata httpGetEnabled=\"true\" /></behavior>");

            var endpoint = new WcfServiceEndpoint
            {
                BindingXml = new BindingXml("basicHttpBinding", "<binding name=\"basicHttpBinding\"><readerQuotas maxArrayLength=\"16384\" /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6"
            };

            var endpoint2 = new WcfServiceEndpoint
            {
                BindingXml = new BindingXml("netTcpBinding", "<binding name=\"netTcpBinding\"><readerQuotas maxArrayLength=\"16384\" /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService2, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6"
            };

            service.Endpoints = new[] { endpoint, endpoint2 };

            return service;
        }

        private WcfService TwoBasicHttpEndpointsServingDifferentSericeContractSharingSameListenUriTestCase()
        {
            var service = CreateService();
            service.ServiceBehaviorXml = new ServiceBehaviorXml(
                "<behavior name=\"TestServiceBehavior\" ><serviceMetadata httpGetEnabled=\"true\" /></behavior>");

            var endpoint = new WcfServiceEndpoint
            {
                BindingXml = new BindingXml("basicHttpBinding", "<binding name=\"basicHttpBinding\"><readerQuotas maxArrayLength=\"16384\" /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6"
            };

            var endpoint2 = new WcfServiceEndpoint
            {
                BindingXml = new BindingXml("basicHttpBinding", "<binding name=\"basicHttpBinding\"><readerQuotas maxArrayLength=\"16384\" /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService2, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6"
            };

            service.Endpoints = new[] { endpoint, endpoint2 };

            return service;
        }

        private WcfService TwoBasicHttpEndpointsServingSameContractSharingSameListenUriTestCase()
        {
            var service = CreateService();
            service.ServiceBehaviorXml = new ServiceBehaviorXml(
                "<behavior name=\"TestServiceBehavior\" ><serviceMetadata httpGetEnabled=\"true\" /></behavior>");

            var endpoint = new WcfServiceEndpoint
            {
                Address = "urn:t1",
                BindingXml = new BindingXml("basicHttpBinding", "<binding name=\"basicHttpBinding\"><readerQuotas maxArrayLength=\"16384\" /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6",
                ListenUri = "",
                ListenUriMode = WcfListenUriMode.Explicit
            };

            var endpoint2 = new WcfServiceEndpoint
            {
                Address = "urn:t2",
                BindingXml = new BindingXml("basicHttpBinding", "<binding name=\"basicHttpBinding\"><readerQuotas maxArrayLength=\"16384\" /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6",
                ListenUri = "",
                ListenUriMode = WcfListenUriMode.Explicit
            };

            service.Endpoints = new[] { endpoint, endpoint2 };

            return service;
        }

        private WcfService NetNamedPipeAndMexNamedPipeEndpointsTestCase()
        {
            var service = CreateService();
            service.ServiceBehaviorXml = new ServiceBehaviorXml(
                "<behavior name=\"TestServiceBehavior\" ><serviceMetadata httpGetEnabled=\"false\" /></behavior>");

            var endpoint = new WcfServiceEndpoint
            {
                BindingXml = new BindingXml("netNamedPipeBinding", "<binding name=\"netTcpBinding\"><readerQuotas maxArrayLength=\"16384\" /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6"
            };

            var endpoint2 = new WcfServiceEndpoint
            {
                Address = "mexipc",
                BindingXml = new BindingXml("mexNamedPipeBinding", null),
                ServiceContractType = "IMetadataExchange"
            };

            service.Endpoints = new[] { endpoint, endpoint2 };

            return service;
        }

        private WcfService CustomEndpointsTestCase()
        {
            var service = CreateService();
            service.ServiceBehaviorXml = new ServiceBehaviorXml(
                "<behavior name=\"TestServiceBehavior\" ><serviceMetadata httpGetEnabled=\"false\" /></behavior>");

            var endpoint = new WcfServiceEndpoint
            {
                BindingXml = new BindingXml("customNamedPipeBinding", "<binding name=\"custom\"><binaryMessageEncoding /><namedPipeTransport /><testBindingExtension /></binding>"),
                ServiceContractType = "NIntegrate.Test.TestClasses.ITestService, NIntegrate.Test, Version=0.9.2.1, Culture=neutral, PublicKeyToken=e2b9e2165dbdd5e6"
            };

            service.Endpoints = new[] { endpoint };

            return service;
        }

        #endregion
    }
}
