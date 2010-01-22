using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Configuration;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// Maps to the &lt;host&gt; configuration element.
    /// </summary>
    [DataContract]
    public sealed class HostXml : ConfigurationXml
    {
        private HostElement _element;
        private Uri[] _baseAddresses;

        #region Constructors

        public HostXml(string xml)
            : base(xml)
        {
        }

        #endregion

        #region Public Methods

        public Uri[] CreateBaseAddresses()
        {
            if (_baseAddresses != null)
                return _baseAddresses;

            lock (SyncLock)
            {
                if (_baseAddresses != null)
                    return _baseAddresses;

                Deserialize();

                var collection = _element.BaseAddresses;
                _baseAddresses = new Uri[collection.Count];
                for (var i = 0; i < collection.Count; ++i)
                {
                    _baseAddresses[i] = new Uri(collection[i].BaseAddress);
                }

                return _baseAddresses;
            }
        }

        public void ApplyHostTimeoutsConfiguration(ServiceHostBase host)
        {
            if (host == null)
                return;

            if (_element == null)
            {
                lock(SyncLock)
                {
                    if (_element == null)
                    {
                        _element = new HostElement();
                        Deserialize(_element);
                    }
                }
            }

            host.OpenTimeout = _element.Timeouts.OpenTimeout;
            host.CloseTimeout = _element.Timeouts.CloseTimeout;
        }

        #endregion

        #region Non-Public Methods

        private void Deserialize()
        {
            _element = new HostElement();
            Deserialize(_element);
        }

        #endregion
    }
}
