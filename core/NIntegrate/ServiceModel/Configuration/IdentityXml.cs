using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Reflection;
using System.ServiceModel.Description;
using System.Diagnostics;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// Maps to the &lt;identityg&gt; configuration element.
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public sealed class IdentityXml : ConfigurationXml
    {
        private EndpointIdentity _identity;

        private static readonly MethodInfo _methodLoadIdentity;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityXml"/> class.
        /// </summary>
        /// <param name="xml">The XML.</param>
        public IdentityXml(string xml)
            : base(xml)
        {
        }

        /// <summary>
        /// Initializes the <see cref="IdentityXml"/> class.
        /// </summary>
        static IdentityXml()
        {
            var type =
                typeof (ContractDescription).Assembly.GetType("System.ServiceModel.Description.ConfigLoader");
            _methodLoadIdentity = type.GetMethod(
                "LoadIdentity",
                BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic);

            Trace.Assert(_methodLoadIdentity != null);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the endpoint identity.
        /// </summary>
        /// <returns></returns>
        public EndpointIdentity CreateEndpointIdentity()
        {
            if (_identity != null)
                return _identity;

            lock (SyncLock)
            {
                if (_identity != null)
                    return _identity;

                var element = new IdentityElement();
                Deserialize(element);
                _identity = (EndpointIdentity) _methodLoadIdentity.Invoke(null, new object[] {element});

                return _identity;
            }
        }

        #endregion
    }
}
