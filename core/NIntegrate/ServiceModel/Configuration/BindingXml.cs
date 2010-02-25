using System;
using System.Collections.Generic;
using System.Configuration;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;
using System.Xml;
using System.Reflection;
using System.Globalization;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// Maps to the &lt;binding&gt; configuration element.
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public sealed class BindingXml : ConfigurationXml
    {
        [DataMember]
        private readonly string _bindingTypeCode;

        private string[] _availableProtocols;
        private Binding _binding;

        private static readonly MethodInfo _methodCreateBindingElement;

        #region Constructors

        public BindingXml(string bindingTypeCode, string xml)
            : base(string.IsNullOrEmpty(xml) ? "<binding name=\"default\" />" : xml)
        {
            if (string.IsNullOrEmpty(bindingTypeCode))
                throw new ArgumentNullException("bindingTypeCode");

            _bindingTypeCode = bindingTypeCode;
        }

        static BindingXml()
        {
            _methodCreateBindingElement = typeof (BindingElementExtensionElement).GetMethod(
                "CreateBindingElement", BindingFlags.Instance | BindingFlags.NonPublic);
        }

        #endregion

        #region Properties

        public string BindingTypeCode
        {
            get { return _bindingTypeCode; }
        }

        #endregion

        #region Public Methods

        public Binding CreateBinding()
        {
            if (_binding != null)
                return _binding;

            lock (SyncLock)
            {
                if (_binding != null)
                    return _binding;

                Deserialize();

                return _binding;
            }
        }

        public string CreateAddress(string path, Uri[] baseAddresses)
        {
            if (path == null)
                path = string.Empty;

            if (IsFullAddress(path))
                return path;

            if (_binding == null)
            {
                lock (SyncLock)
                {
                    if (_binding == null)
                        Deserialize();
                }
            }

            if (baseAddresses != null && baseAddresses.Length > 0)
            {
                foreach (var baseAddress in baseAddresses)
                {
                    var address = baseAddress.ToString();
                    foreach (var protocol in _availableProtocols)
                    {
                        if (address.StartsWith(protocol + "://", StringComparison.OrdinalIgnoreCase))
                        {
                            if (IsAbsolutePath(path))
                            {
                                if (path.StartsWith("/", StringComparison.Ordinal))
                                    return protocol + "://" + baseAddress.Host + ":" + baseAddress.Port + path;
                                if (path.StartsWith(":", StringComparison.Ordinal))
                                    return protocol + "://" + baseAddress.Host + path;
                            }
                            else
                            {
                                return (address.TrimEnd('/') + "/" + path).TrimEnd('/');
                            }
                        }
                    }
                }
            }

            return path;
        }

        #endregion

        #region Non-Public Methods

        private static bool IsAbsolutePath(string path)
        {
            return path.StartsWith("/", StringComparison.Ordinal) || path.StartsWith(":", StringComparison.Ordinal);
        }

        private static bool IsFullAddress(string path)
        {
            return path.Contains("://");
        }

        private static List<BindingElementExtensionElement> FilterCustomBindingElementExtensionElements(XmlDocument doc)
        {
            var customBindingElementExtensionElements = new List<BindingElementExtensionElement>();
            var customBindingElementExtensionNodes = new List<XmlNode>();

            foreach (XmlNode node in doc.FirstChild.ChildNodes)
            {
                var customBindingElementExtensionElementType = BindingElementExtensionRegistry.Instance[node.Name.ToLowerInvariant()];
                if (customBindingElementExtensionElementType != null)
                {
                    var customBindingElementExtensionElement = Activator.CreateInstance(customBindingElementExtensionElementType) as BindingElementExtensionElement;
                    if (customBindingElementExtensionElement == null)
                    {
                        throw new ConfigurationErrorsException(
                            string.Format(
                                CultureInfo.InvariantCulture,
                                SR.SPECIFIED_BINDING_ELEMENT_EXTENSION_CONFIGURATION_ELEMENT_COULD_NOT_BE_INITEDa,
                                customBindingElementExtensionElementType));
                    }
                    Deserialize(node.OuterXml, customBindingElementExtensionElement);
                    customBindingElementExtensionElements.Add(customBindingElementExtensionElement);
                    customBindingElementExtensionNodes.Add(node);
                }
            }

            foreach (var node in customBindingElementExtensionNodes)
            {
                node.ParentNode.RemoveChild(node);
            }

            return customBindingElementExtensionElements;
        }

        private static BindingElement CreateBindingElement(BindingElementExtensionElement item)
        {
            return _methodCreateBindingElement.Invoke(item, null) as BindingElement;
        }

        private void Deserialize()
        {
            var bindingTypeDescr = BindingTypeRegistry.Instance[_bindingTypeCode];
            if (bindingTypeDescr == null)
                throw new KeyNotFoundException(_bindingTypeCode);

            _availableProtocols = bindingTypeDescr.AvailableProtocols;

            _binding = Activator.CreateInstance(bindingTypeDescr.BindingType) as Binding;

            var element =
                Activator.CreateInstance(bindingTypeDescr.BindingConfigurationElementType);
            var standardBindingElement = element as StandardBindingElement;
            if (standardBindingElement == null) //means it is a custom binding element
            {
                var customBindingElement = element as CustomBindingElement;
                if (customBindingElement != null)
                {
                    var customBinding = new CustomBinding(_binding);
                    _binding = customBinding;

                    var doc = new XmlDocument();
                    doc.LoadXml(Xml);
                    var customBindingElementExtensionElements = FilterCustomBindingElementExtensionElements(doc);
                    Deserialize(doc.OuterXml, customBindingElement);
                    customBindingElement.ApplyConfiguration(_binding);
                    foreach (var item in customBindingElementExtensionElements)
                    {
                        customBinding.Elements.Insert(0, CreateBindingElement(item));
                    }
                }
            }
            else
            {
                Deserialize(standardBindingElement);
                if (_binding != null)
                    standardBindingElement.ApplyConfiguration(_binding);
            }
        }

        #endregion
    }
}
