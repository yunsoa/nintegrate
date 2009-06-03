using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ServiceModel.Configuration;
using System.Xml;
using System.IO;
using NIntegrate.Configuration;
using NIntegrate.Test.DatabaseScripts.TestClasses;
using System.ServiceModel.Description;

namespace NIntegrate.Test.DatabaseScripts
{
    [TestClass]
    public class ConfigurationElementDeserializationTest
    {
        [TestMethod]
        public void TestIdentityElementDeserialization()
        {
            var identityXML = "<identity><userPrincipalName value=\"host\\myservice.com\" /></identity>";
            var elem = new IdentityElement();
            elem.DeserializeElement(identityXML);
            Assert.AreEqual("host\\myservice.com", elem.UserPrincipalName.Value);
        }

        [TestMethod]
        public void TestHostElementDeserialization()
        {
            var hostXML = "<host><baseAddresses><add baseAddress=\"http://localhost:8080/ServiceMetadata\" /></baseAddresses></host>";
            var elem = new HostElement();
            elem.DeserializeElement(hostXML);
            Assert.AreEqual("http://localhost:8080/ServiceMetadata", elem.BaseAddresses[0].BaseAddress);
        }

        [TestMethod]
        public void TestServiceBehaviorElementDeserialization()
        {
            var serviceBehaviorXML = "<behavior name=\"metadataSupport\"><serviceMetadata httpGetEnabled=\"true\" httpGetUrl=\"\"/></behavior>";
            var elem = new ServiceBehaviorElement();
            elem.DeserializeElement(serviceBehaviorXML);
            Assert.AreEqual("metadataSupport", elem.Name);
            Assert.AreEqual(1, elem.Count);
        }

        [TestMethod]
        public void TestServiceBehaviorElementDeserializationWithCustomServiceBehavior()
        {
            var serviceBehaviorXML = "<behavior name=\"custom\"><serviceMetadata httpGetEnabled=\"true\" httpGetUrl=\"\"/><testCustomService /></behavior>";
            var doc = new XmlDocument();
            doc.LoadXml(serviceBehaviorXML);
            foreach (XmlNode node in doc.FirstChild.ChildNodes)
            {
                if (node.Name == "testCustomService")
                {
                    var testElem = new TestCustomServiceBehaviorElement();
                    testElem.DeserializeElement(node.OuterXml);
                    node.ParentNode.RemoveChild(node);
                    break;
                }
            }
            var elem = new ServiceBehaviorElement();
            elem.DeserializeElement(doc.OuterXml);
        }

        [TestMethod]
        public void TestBindingElementDeserialization()
        {
            var bindingXML = "<binding name = \"TransactionalTCP\" transactionFlow = \"true\"/>";
            var elem = new NetTcpBindingElement();
            elem.DeserializeElement(bindingXML);
            Assert.AreEqual("TransactionalTCP", elem.Name);
            Assert.IsTrue(elem.TransactionFlow);
        }
    }
}
