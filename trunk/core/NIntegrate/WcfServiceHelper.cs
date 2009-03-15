using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using NIntegrate.Configuration;

namespace NIntegrate
{
    internal static class WcfServiceHelper
    {
        /// <summary>
        /// Get all implemented interfaces of the service implementation type 
        /// marked with ServiceContractAttribute
        /// </summary>
        /// <param name="serviceType">The service implementation type</param>
        /// <returns></returns>
        internal static List<Type> GetServiceContracts(Type serviceType)
        {
            var list = new List<Type>();
            foreach (var type in serviceType.GetInterfaces())
            {
                var attrs = type.GetCustomAttributes(typeof(ServiceContractAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    list.Add(type);
            }

            return list;
        }

        /// <summary>
        /// Get the version string of the assembly
        /// </summary>
        /// <param name="assembly">The assembly</param>
        /// <returns></returns>
        internal static string GetServiceVersion(System.Reflection.Assembly assembly)
        {
            var posStart = assembly.FullName.IndexOf("Version=") + "Version=".Length;
            var posEnd = assembly.FullName.IndexOf(",", posStart);
            return assembly.FullName.Substring(posStart, posEnd - posStart);
        }

        /// <summary>
        /// Build Wcf service binding from service contract and endpoint config
        /// </summary>
        /// <param name="serviceContract">The service contract</param>
        /// <param name="endpoint">The endpoint config</param>
        /// <returns></returns>
        internal static Binding BuildBinding(Type serviceContract, Endpoint endpoint)
        {
            Binding binding = null;
            if (endpoint.ChannelType == typeof(WSHttpBinding).Name)
            {
                var sm = (SecurityMode)Enum.Parse(typeof(SecurityMode), endpoint.SecurityMode);
                var wsBinding = new WSHttpBinding(sm)
                                    {
                                        MaxBufferPoolSize = endpoint.MaxBufferPoolSize,
                                        MaxReceivedMessageSize = endpoint.MaxReceivedMessageSize,
                                        TransactionFlow = endpoint.TransactionFlow
                                    };

                if (endpoint.ClientCredentialTypeName != "None")
                {
                    try
                    {
                        var mct = (MessageCredentialType)Enum.Parse(typeof(MessageCredentialType), endpoint.ClientCredentialTypeName);
                        wsBinding.Security.Message.ClientCredentialType = mct;
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        var hcct = (HttpClientCredentialType)Enum.Parse(typeof(HttpClientCredentialType), endpoint.ClientCredentialTypeName);
                        wsBinding.Security.Transport.ClientCredentialType = hcct;
                    }
                    catch (Exception)
                    {
                    }
                }

                if (endpoint.ReliableSessionEnabled)
                {
                    ConfigureReliableSession(endpoint, wsBinding.ReliableSession);
                }

                binding = wsBinding;
            }
            else if (endpoint.ChannelType == typeof(WSFederationHttpBinding).Name)
            {
                var smf = (WSFederationHttpSecurityMode)Enum.Parse(typeof(WSFederationHttpSecurityMode), endpoint.SecurityMode);
                var wsfBinding = new WSFederationHttpBinding(smf)
                {
                    MaxBufferPoolSize = endpoint.MaxBufferPoolSize,
                    MaxReceivedMessageSize = endpoint.MaxReceivedMessageSize,
                    TransactionFlow = endpoint.TransactionFlow
                };

                if (endpoint.ReliableSessionEnabled)
                {
                    ConfigureReliableSession(endpoint, wsfBinding.ReliableSession);
                }

                binding = wsfBinding;
            }
            else if (endpoint.ChannelType == typeof(WSDualHttpBinding).Name)
            {
                var smd = (WSDualHttpSecurityMode)Enum.Parse(typeof(WSDualHttpSecurityMode), endpoint.SecurityMode);
                var wsdBinding = new WSDualHttpBinding(smd)
                {
                    MaxBufferPoolSize = endpoint.MaxBufferPoolSize,
                    MaxReceivedMessageSize = endpoint.MaxReceivedMessageSize,
                    TransactionFlow = endpoint.TransactionFlow
                };

                if (endpoint.ClientCredentialTypeName != "None")
                {
                    try
                    {
                        var mct = (MessageCredentialType)Enum.Parse(typeof(MessageCredentialType), endpoint.ClientCredentialTypeName);
                        wsdBinding.Security.Message.ClientCredentialType = mct;
                    }
                    catch (Exception)
                    {
                    }
                }

                binding = wsdBinding;
            }
            else if (endpoint.ChannelType == typeof(WebHttpBinding).Name)
            {
                var smw = (WebHttpSecurityMode)Enum.Parse(typeof(WebHttpSecurityMode), endpoint.SecurityMode);
                var webBinding = new WebHttpBinding(smw)
                {
                    MaxBufferPoolSize = endpoint.MaxBufferPoolSize,
                    MaxReceivedMessageSize = endpoint.MaxReceivedMessageSize,
                    MaxBufferSize = endpoint.MaxBufferSize,
                    TransferMode =
                        ((TransferMode)Enum.Parse(typeof(TransferMode), endpoint.TransferMode)),
                };

                if (endpoint.ClientCredentialTypeName != "None")
                {
                    try
                    {
                        var hcct = (HttpClientCredentialType)Enum.Parse(typeof(HttpClientCredentialType), endpoint.ClientCredentialTypeName);
                        webBinding.Security.Transport.ClientCredentialType = hcct;
                    }
                    catch (Exception)
                    {
                    }
                }

                binding = webBinding;
            }
            else if (endpoint.ChannelType == typeof(BasicHttpBinding).Name)
            {
                var smb = (BasicHttpSecurityMode)Enum.Parse(typeof(BasicHttpSecurityMode), endpoint.SecurityMode);
                var basicBinding = new BasicHttpBinding(smb)
                {
                    MaxBufferPoolSize = endpoint.MaxBufferPoolSize,
                    MaxReceivedMessageSize = endpoint.MaxReceivedMessageSize,
                    MaxBufferSize = endpoint.MaxBufferSize,
                    TransferMode =
                        ((TransferMode)Enum.Parse(typeof(TransferMode), endpoint.TransferMode)),
                };

                if (endpoint.ClientCredentialTypeName != "None")
                {
                    try
                    {
                        var bhmct = (BasicHttpMessageCredentialType)Enum.Parse(typeof(BasicHttpMessageCredentialType), endpoint.ClientCredentialTypeName);
                        basicBinding.Security.Message.ClientCredentialType = bhmct;
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        var hcct = (HttpClientCredentialType)Enum.Parse(typeof(HttpClientCredentialType), endpoint.ClientCredentialTypeName);
                        basicBinding.Security.Transport.ClientCredentialType = hcct;
                    }
                    catch (Exception)
                    {
                    }
                }

                binding = basicBinding;
            }
            else if (endpoint.ChannelType == typeof(NetMsmqBinding).Name)
            {
                var smq = (NetMsmqSecurityMode)Enum.Parse(typeof(NetMsmqSecurityMode), endpoint.SecurityMode);
                var mqBinding = new NetMsmqBinding(smq)
                {
                    MaxBufferPoolSize = endpoint.MaxBufferPoolSize,
                    MaxReceivedMessageSize = endpoint.MaxReceivedMessageSize,
                };

                if (endpoint.ClientCredentialTypeName != "None")
                {
                    try
                    {
                        var mct = (MessageCredentialType)Enum.Parse(typeof(MessageCredentialType), endpoint.ClientCredentialTypeName);
                        mqBinding.Security.Message.ClientCredentialType = mct;
                    }
                    catch (Exception)
                    {
                    }
                }

                binding = mqBinding;
            }
            else if (endpoint.ChannelType == typeof(NetTcpBinding).Name)
            {
                var sm = (SecurityMode)Enum.Parse(typeof(SecurityMode), endpoint.SecurityMode);
                var tcpBinding = new NetTcpBinding(sm)
                                     {
                                         MaxBufferPoolSize = endpoint.MaxBufferPoolSize,
                                         MaxReceivedMessageSize = endpoint.MaxReceivedMessageSize,
                                         ListenBacklog = endpoint.ListenBacklog,
                                         MaxBufferSize = endpoint.MaxBufferSize,
                                         MaxConnections = endpoint.MaxConnections,
                                         PortSharingEnabled = endpoint.PortSharingEnabled,
                                         TransferMode =
                                             ((TransferMode) Enum.Parse(typeof (TransferMode), endpoint.TransferMode)),
                                         TransactionFlow = endpoint.TransactionFlow
                                     };

                if (endpoint.ClientCredentialTypeName != "None")
                {
                    try
                    {
                        var mct = (MessageCredentialType)Enum.Parse(typeof(MessageCredentialType), endpoint.ClientCredentialTypeName);
                        tcpBinding.Security.Message.ClientCredentialType = mct;
                    }
                    catch (Exception)
                    {
                    }
                    try
                    {
                        var tcct = (TcpClientCredentialType)Enum.Parse(typeof(TcpClientCredentialType), endpoint.ClientCredentialTypeName);
                        tcpBinding.Security.Transport.ClientCredentialType = tcct;
                    }
                    catch (Exception)
                    {
                    }
                }

                if (endpoint.ReliableSessionEnabled)
                {
                    ConfigureReliableSession(endpoint, tcpBinding.ReliableSession);
                }

                binding = tcpBinding;
            }
            else if (endpoint.ChannelType == typeof(NetNamedPipeBinding).Name)
            {
                var namedPipeBinding = new NetNamedPipeBinding(
                    (NetNamedPipeSecurityMode)Enum.Parse(typeof(NetNamedPipeSecurityMode),
                    endpoint.SecurityMode))
                                           {
                                               MaxBufferPoolSize = endpoint.MaxBufferPoolSize,
                                               MaxReceivedMessageSize = endpoint.MaxReceivedMessageSize,
                                               MaxBufferSize = endpoint.MaxBufferSize,
                                               MaxConnections = endpoint.MaxConnections,
                                               TransferMode =
                                                   ((TransferMode)
                                                    Enum.Parse(typeof (TransferMode), endpoint.TransferMode)),
                                               TransactionFlow = endpoint.TransactionFlow
                                           };

                binding = namedPipeBinding;
            }
            if (binding != null)
            {
                binding.Name = serviceContract.ToString();
                binding.OpenTimeout = new TimeSpan(0, 0, endpoint.OpenTimeout);
                binding.CloseTimeout = new TimeSpan(0, 0, endpoint.CloseTimeout);
                binding.ReceiveTimeout = new TimeSpan(0, 0, endpoint.ReceiveTimeout);
                binding.SendTimeout = new TimeSpan(0, 0, endpoint.SendTimeout);
            }
            return binding;
        }

        private static void ConfigureReliableSession(Endpoint endpoint, OptionalReliableSession reliableSession)
        {
            reliableSession.Enabled = true;
            reliableSession.InactivityTimeout = 
                TimeSpan.FromSeconds(endpoint.ReliableSessionInactivityTimeout);
            reliableSession.Ordered = endpoint.ReliableSessionOrdered;
        }

        /// <summary>
        /// Build service address from endpoint config 
        /// and the optional specified baseAddresses
        /// </summary>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        internal static Uri BuildAddress(Endpoint endpoint)
        {
            //create & return an address according to the endpoint config
            var address = default(Uri);
            if (endpoint.ChannelType.Contains("Http"))
                return new Uri((endpoint.ClientCredentialTypeName == "Certificate" ? "https://" : "http://") + endpoint.FarmAddress + endpoint.Address);

            if (endpoint.ChannelType == typeof(NetMsmqBinding).Name)
                return new Uri("net.msmq://localhost" + endpoint.Address);

            if (endpoint.ChannelType == typeof(NetTcpBinding).Name)
                return new Uri("net.tcp://" + endpoint.FarmAddress + endpoint.Address);

            if (endpoint.ChannelType == typeof(NetNamedPipeBinding).Name)
                return new Uri("net.pipe://localhost" + endpoint.Address);

            return address;
        }
    }
}
