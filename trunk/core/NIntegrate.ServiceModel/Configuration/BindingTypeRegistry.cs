using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.ServiceModel.Description;
using System.ServiceModel.MsmqIntegration;
using NIntegrate.ServiceModel.Collections.Generic;

namespace NIntegrate.ServiceModel.Configuration
{
    public sealed class BindingTypeRegistry : Registry<string, BindingTypeDescription>
    {
        public static readonly BindingTypeRegistry Instance;

        #region Constructors

        private BindingTypeRegistry()
        {
            LoadBuildInBindingTypes();
        }

        static BindingTypeRegistry()
        {
            Instance = new BindingTypeRegistry();
        }

        #endregion

        #region Properties

        public override BindingTypeDescription this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key))
                    throw new ArgumentNullException("key");

                key = key.ToLowerInvariant();

                return base[key];
            }
        }

        #endregion

        #region Public Methods

        public override bool AddItem(string key, BindingTypeDescription value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");
            if (value.BindingType == null)
                throw new ArgumentNullException("value.BindingType");
            if (value.BindingConfigurationElementType == null)
                throw new ArgumentNullException("value.BindingConfigurationElementType");
            if (value.AvailableProtocols == null || value.AvailableProtocols.Length == 0)
                throw new ArgumentNullException("value.AvailableProtocols");

            key = key.ToLowerInvariant();

            return base.AddItem(key, value);
        }

        #endregion

        #region Non-Public Methods

        private void LoadBuildInBindingTypes()
        {
            AddItem("basichttpbinding", new BindingTypeDescription { BindingType = typeof(BasicHttpBinding), BindingConfigurationElementType = typeof(BasicHttpBindingElement), AvailableProtocols = new[] { "http", "https" } });
            AddItem("basichttpcontextbinding", new BindingTypeDescription { BindingType = typeof(BasicHttpContextBinding), BindingConfigurationElementType = typeof(BasicHttpContextBindingElement), AvailableProtocols = new[] { "http", "https" } });
            AddItem("webhttpbinding", new BindingTypeDescription { BindingType = typeof(WebHttpBinding), BindingConfigurationElementType = typeof(WebHttpBindingElement), AvailableProtocols = new[] { "http", "https" } });
            AddItem("wshttpbinding", new BindingTypeDescription { BindingType = typeof(WSHttpBinding), BindingConfigurationElementType = typeof(WSHttpBindingElement), AvailableProtocols = new[] { "http", "https" } });
            AddItem("wsdualhttpbinding", new BindingTypeDescription { BindingType = typeof(WSDualHttpBinding), BindingConfigurationElementType = typeof(WSDualHttpBindingElement), AvailableProtocols = new[] { "http", "https" } });
            AddItem("wsfederationhttpbinding", new BindingTypeDescription { BindingType = typeof(WSFederationHttpBinding), BindingConfigurationElementType = typeof(WSFederationHttpBindingElement), AvailableProtocols = new[] { "http", "https" } });
            AddItem("customhttpbinding", new BindingTypeDescription { BindingType = typeof(CustomBinding), BindingConfigurationElementType = typeof(CustomBindingElement), AvailableProtocols = new[] { "http", "https" } });
            AddItem("mexHttpBinding", new BindingTypeDescription { BindingType = typeof(WSHttpBinding), BindingConfigurationElementType = typeof(CustomBindingElement), AvailableProtocols = new[] { "http" } });
            AddItem("mexHttpsBinding", new BindingTypeDescription { BindingType = typeof(WSHttpBinding), BindingConfigurationElementType = typeof(CustomBindingElement), AvailableProtocols = new[] { "https" } });
            AddItem("nettcpbinding", new BindingTypeDescription { BindingType = typeof(NetTcpBinding), BindingConfigurationElementType = typeof(NetTcpBindingElement), AvailableProtocols = new[] { "net.tcp" } });
            AddItem("nettcpcontextbinding", new BindingTypeDescription { BindingType = typeof(NetTcpContextBinding), BindingConfigurationElementType = typeof(NetTcpContextBindingElement), AvailableProtocols = new[] { "net.tcp" } });
            AddItem("netpeertcpbinding", new BindingTypeDescription { BindingType = typeof(NetPeerTcpBinding), BindingConfigurationElementType = typeof(NetPeerTcpBindingElement), AvailableProtocols = new[] { "net.p2p" } });
            AddItem("customtcpbinding", new BindingTypeDescription { BindingType = typeof(CustomBinding), BindingConfigurationElementType = typeof(CustomBindingElement), AvailableProtocols = new[] { "net.tcp", "net.p2p" } });
            AddItem("mexTcpBinding", new BindingTypeDescription { BindingType = typeof(NetTcpBinding), BindingConfigurationElementType = typeof(CustomBindingElement), AvailableProtocols = new[] { "net.tcp" } });
            AddItem("netnamedpipebinding", new BindingTypeDescription { BindingType = typeof(NetNamedPipeBinding), BindingConfigurationElementType = typeof(NetNamedPipeBindingElement), AvailableProtocols = new[] { "net.pipe" } });
            AddItem("customnamedpipebinding", new BindingTypeDescription { BindingType = typeof(CustomBinding), BindingConfigurationElementType = typeof(CustomBindingElement), AvailableProtocols = new[] { "net.pipe" } });
            AddItem("mexNamedPipeBinding", new BindingTypeDescription { BindingType = typeof(NetNamedPipeBinding), BindingConfigurationElementType = typeof(CustomBindingElement), AvailableProtocols = new[] { "net.pipe" } });
            AddItem("netmsmqbinding", new BindingTypeDescription { BindingType = typeof(NetMsmqBinding), BindingConfigurationElementType = typeof(NetMsmqBindingElement), AvailableProtocols = new[] { "net.msmq" } });
            AddItem("msmqintegrationbinding", new BindingTypeDescription { BindingType = typeof(MsmqIntegrationBinding), BindingConfigurationElementType = typeof(System.ServiceModel.Configuration.MsmqIntegrationBindingElement), AvailableProtocols = new[] { "msmq.formatname" } });
            AddItem("custommsmqbinding", new BindingTypeDescription { BindingType = typeof(CustomBinding), BindingConfigurationElementType = typeof(CustomBindingElement), AvailableProtocols = new[] { "net.msmq", "msmq.formatname" } });
        }

        #endregion
    }
}