using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NIntegrate.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel;
using NIntegrate.Test.TestClasses;

namespace NIntegrate.Test
{
    [TestClass]
    public class WcfConfigurationXmlTest
    {
        [TestMethod]
        public void TestEndpointBehaviorXml()
        {
            string xml = "<behavior name=\"String\">\r\n" +
                "<callbackDebug\r\n" +
                "includeExceptionDetailInFaults=\"false\"\r\n" +
                "/>\r\n" +
                "<callbackTimeOuts\r\n" +
                "transactionTimeout=\"00:00:10\"\r\n" +
                "/>\r\n" +
                "<clientVia\r\n" +
                "viaUri=\"Uri\"\r\n" +
                "/>\r\n" +
                "<synchronousReceive />\r\n" +
                "<transactedBatching\r\n" +
                "maxBatchSize=\"0\"\r\n" +
                "/>\r\n" +
                "</behavior>";
            var endpointBehaviorXml = new EndpointBehaviorXml(xml);
            var contract = new ContractDescription("test");
            var endpoint = new ServiceEndpoint(contract);
            endpointBehaviorXml.ApplyEndpointBehaviorConfiguration(endpoint);
        }

        [TestMethod]
        public void TestServiceBehaviorXml()
        {
            string xml = "<behavior name=\"String\">\r\n" +
                "<serviceMetadata\r\n" +
                "httpGetEnabled=\"false\"\r\n" +
                "httpGetUrl=\"Uri\"\r\n" +
                "httpsGetEnabled=\"false\"\r\n" +
                "httpsGetUrl=\"Uri\"\r\n" +
                "externalMetadataLocation=\"Uri\"\r\n" +
                "/>\r\n" +
                "<serviceDebug\r\n" +
                "httpHelpPageEnabled=\"false\"\r\n" +
                "httpHelpPageUrl=\"Uri\"\r\n" +
                "httpsHelpPageEnabled=\"false\"\r\n" +
                "httpsHelpPageUrl=\"Uri\"\r\n" +
                "includeExceptionDetailInFaults=\"false\"\r\n" +
                "/>\r\n" +
                "<serviceThrottling\r\n" +
                "maxConcurrentCalls=\"16\"\r\n" +
                "maxConcurrentInstances=\"26\"\r\n" +
                "maxConcurrentSessions=\"10\"\r\n" +
                "/>\r\n" +
                "<serviceTimeOuts\r\n" +
                "transactionTimeout=\"0\"\r\n" +
                "/>\r\n" +
                "</behavior>";
            var serviceBehaviorXml = new ServiceBehaviorXml(xml);
            var servcieHost = new ServiceHost(typeof(TestServiceImpl));
            serviceBehaviorXml.ApplyServiceBehaviorConfiguration(servcieHost);
        }

        [TestMethod]
        public void TestBindingXml()
        {
            string xml = "<binding\r\n" +
                "name=\"String\"\r\n" +
                "closeTimeout=\"00:01:00\"\r\n" +
                "openTimeout=\"00:01:00\"\r\n" +
                "receiveTimeout=\"00:10:00\"\r\n" +
                "sendTimeout=\"00:01:00\"\r\n" +
              ">\r\n" +
                "<compositeDuplex clientBaseAddress=\"Uri\"/>\r\n" +
                "<pnrpPeerResolver />\r\n" +
                "<reliableSession\r\n" +
                  "acknowledgementInterval=\"00:00:0.2\"\r\n" +
                  "flowControlEnabled=\"true\"\r\n" +
                  "inactivityTimeout=\"00:10:00\"\r\n" +
                  "maxPendingChannels=\"128\"\r\n" +
                  "maxRetryCount=\"8\"\r\n" +
                  "maxTransferWindowSize=\"32\"\r\n" +
                  "ordered=\"true\"\r\n" +
                "/>\r\n" +
                "<sslStreamSecurity requireClientCertificate=\"false\" />\r\n" +
                "<transactionFlow transactionProtocol=\"OleTransactions\"/>\r\n" +
                "<security\r\n" +
                "  allowSerializedSigningTokenOnReply=\"false\"\r\n" +
                "  authenticationMode=\"SspiNegotiated\"\r\n" +
                "  includeTimestamp=\"true\"\r\n" +
                "  keyEntropyMode=\"CombinedEntropy\"\r\n" +
                "  messageProtectionOrder=\"SignBeforeEncrypt\"\r\n" +
                "  requireDerivedKeys=\"true\"\r\n" +
                "  requireSecurityContextCancellation=\"true\"\r\n" +
                "  requireSignatureConfirmation=\"false\"\r\n" +
                "  securityHeaderLayout=\"Strict\"\r\n" +
                ">\r\n" +
                "  <localClientSettings\r\n" +
                "    cacheCookies=\"false\"\r\n" +
                "    cookieRenewalThresholdPercentage=\"90\"\r\n" +
                "    detectReplays=\"false\"\r\n" +
                "    maxClockSkew=\"00:05:00\"\r\n" +
                "    maxCookieCachingTime=\"10675199.02:48:05.4775807\"\r\n" +
                "    reconnectTransportOnFailure=\"true\"\r\n" +
                "    replayCacheSize=\"500000\"\r\n" +
                "    replayWindow=\"00:05:00\"\r\n" +
                "    sessionKeyRenewalInterval=\"10:00:00\"\r\n" +
                "    sessionKeyRolloverInterval=\"00:05:00\"\r\n" +
                "    timestampValidityDuration=\"00:15:00\"\r\n" +
                "  />\r\n" +
                "  <localServiceSettings\r\n" +
                "    detectReplays=\"false\"\r\n" +
                "    inactivityTimeout=\"01:00:00\"\r\n" +
                "    maxCachedCookies=\"1000\"\r\n" +
                "    maxClockSkew=\"00:05:00\"\r\n" +
                "    maxPendingSessions=\"1000\"\r\n" +
                "    maxStatefulNegotiations=\"1024\"\r\n" +
                "    negotiationTimeout=\"00:02:00\"\r\n" +
                "    reconnectTransportOnFailure=\"true\"\r\n" +
                "    replayCacheSize=\"500000\"\r\n" +
                "    replayWindow=\"00:05:00\"\r\n" +
                "    sessionKeyRenewalInterval=\"10:00:00\"\r\n" +
                "    sessionKeyRolloverInterval=\"00:05:00\"\r\n" +
                "    timestampValidityDuration=\"00:15:00\"\r\n" +
                "  />\r\n" +
                "</security>\r\n" +
              "</binding>";

            var bindingXml = new BindingXml("customhttpbinding", xml);
            var binding = bindingXml.CreateBinding();
        }

        [TestMethod]
        public void TestHeadersXml()
        {
            string xml = "<headers>\r\n" +
            "    <Region xmlns=\"Uri\">String</Region>\r\n" +
            "        <Member xmlns=\"Uri\">String</Member>\r\n" +
            "</headers>";

            var headersXml = new HeadersXml(xml);
            var headers = headersXml.CreateAddressHeaders();
        }

        [TestMethod]
        public void TestHostXml()
        {
            string xml = "<host>\r\n" +
            "  <baseAddresses>\r\n" +
            "    <add baseAddress=\"String\" />\r\n" +
            "  </baseAddresses>\r\n" +
            "</host>";

            var hostXml = new HostXml(xml);
            var servcieHost = new ServiceHost(typeof(TestServiceImpl));
            hostXml.ApplyHostTimeoutsConfiguration(servcieHost);
        }

        [TestMethod]
        public void TestIdentityXml()
        {
            string xml = "<identity>\r\n" +
            "  <servicePrincipalName value=\"String\"/>\r\n" +
            "  <certificateReference\r\n" +
            "    findValue=\"String\"\r\n" +
            "    isChainIncluded=\"false\"\r\n" +
            "    storeName=\"AddressBook\"\r\n" +
            "    storeLocation=\"LocalMachine\"\r\n" +
            "  />\r\n" +
            "</identity>";

            var identityXml = new IdentityXml(xml);
            var identity = identityXml.CreateEndpointIdentity();
        }

        [TestMethod]
        public void TestMetadataXml()
        {
            string xml = "<metadata>\r\n" +
            "           <policyImporters>\r\n" +
            "           </policyImporters>\r\n" +
            "         <wsdlImporters>\r\n" +
            "         </wsdlImporters>\r\n" +
            "</metadata>";

            var metadataXml = new MetadataXml(xml);
            var metadataSet = new MetadataSet();
            var importer = new WsdlImporter(metadataSet);
            metadataXml.ApplyPolicyImportersConfiguration(importer);
            metadataXml.ApplyWsdlImportersConfiguration(importer);
        }
    }
}
