using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Reflection;
using System.ServiceModel.Configuration;
using System.Xml;
using System.Runtime.Serialization;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// The base behaviorXml class.
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public class BehaviorXml : ConfigurationXml
    {
        private static readonly MethodInfo _methodCreateBehavior;

        #region Constructors

        internal BehaviorXml(string xml)
            : base(xml)
        {
        }

        static BehaviorXml()
        {
            _methodCreateBehavior
                = typeof(BehaviorExtensionElement).GetMethod("CreateBehavior", BindingFlags.NonPublic | BindingFlags.Instance);
        }

        #endregion

        #region Non-Public Methods

        /// <summary>
        /// Filters the custom behavior elements.
        /// </summary>
        /// <param name="doc">The doc.</param>
        /// <returns></returns>
        protected static List<BehaviorExtensionElement> FilterCustomBehaviorElements(XmlDocument doc)
        {
            if (doc == null)
                throw new ArgumentNullException("doc");

            var customBehaviorElements = new List<BehaviorExtensionElement>();
            var customBehaviorNodes = new List<XmlNode>();
            foreach (XmlNode node in doc.FirstChild.ChildNodes)
            {
                var behaviorElementType = BehaviorExtensionRegistry.Instance[node.Name];
                if (behaviorElementType == null)
                {
                    continue;
                }
                var customBehaviorElement = Activator.CreateInstance(behaviorElementType) as BehaviorExtensionElement;
                if (customBehaviorElement == null)
                {
                    throw new ConfigurationErrorsException(
                        string.Format(
                            CultureInfo.InvariantCulture,
                            SR.SPECIFIED_BEHAVIOR_CONFIGURATION_ELEMENT_COULD_NOT_BE_INITED,
                            behaviorElementType));
                }
                Deserialize(node.OuterXml, customBehaviorElement);
                customBehaviorElements.Add(customBehaviorElement);
                customBehaviorNodes.Add(node);
            }

            foreach (var node in customBehaviorNodes)
            {
                node.ParentNode.RemoveChild(node);
            }

            return customBehaviorElements;
        }

        /// <summary>
        /// Sets the behavior.
        /// </summary>
        /// <typeparam name="TBehavior">The type of the behavior.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="element">The element.</param>
        protected static void SetBehavior<TBehavior>(ICollection<TBehavior> collection, BehaviorExtensionElement element)
        {
            if (collection == null)
                throw new ArgumentNullException("collection");
            if (element == null)
                throw new ArgumentNullException("element");

            var behavior = (TBehavior)_methodCreateBehavior.Invoke(element, null);
            foreach (var item in collection)
            {
                if (item.GetType() == behavior.GetType())
                {
                    collection.Remove(item);
                    break;
                }
            }

            collection.Add(behavior);
        }

        #endregion
    }
}
