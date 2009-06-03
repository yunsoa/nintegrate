using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.Test.TestClasses;

namespace NIntegrate.Test
{
    [TestClass]
    public class ServiceManagerTest
    {
        [TestMethod]
        public void TestServiceManager()
        {
            var locator = ServiceManager.GetServiceLocator<IExternalService1>();
            Assert.IsNotNull(locator);
            Assert.AreEqual(typeof(TestServiceLocator), locator.GetType());

            Assert.IsNotNull(locator.GetService<IExternalService1>());
            Assert.IsNotNull(locator.GetService<IExternalService2>());
            Assert.IsNotNull(locator.GetService<IExternalService1>());

            locator.Dispose();

            locator = ServiceManager.GetServiceLocator(typeof(ITestService));
            Assert.IsNotNull(locator);
            Assert.AreEqual(typeof(WcfServiceLocator), locator.GetType());

            Assert.IsNotNull(locator.GetService<ITestService>());

            locator.Dispose();

            locator = ServiceManager.GetServiceLocator<IExternalService1>();
            Assert.IsNotNull(locator);
            Assert.AreEqual(typeof(TestServiceLocator), locator.GetType());

            Assert.IsNotNull(locator.GetService<IExternalService1>());
            Assert.IsNotNull(locator.GetService<IExternalService2>());
            Assert.IsNotNull(locator.GetService<IExternalService1>());

            locator.Dispose();

            locator = ServiceManager.GetServiceLocator(typeof(ITestService));
            Assert.IsNotNull(locator);
            Assert.AreEqual(typeof(WcfServiceLocator), locator.GetType());

            Assert.IsNotNull(locator.GetService<ITestService>());

            locator.Dispose();
        }
    }
}
