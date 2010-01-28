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
    [DataContract]
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

        protected static List<BehaviorExtensionElement> FilterCustomBehaviorElements(XmlDocument doc)
        {
            var customBehaviorElements = new List<BehaviorExtensionElement>();
            var customBehaviorNodes = new List<XmlNode>();
            foreach (XmlNode node in doc.FirstChild.ChildNodes)
            {
                var behaviorElementType = BehaviorExtensionRegistry.Instance[node.Name];
                if (behaviorElementType == null)
                {
                    //throw new ConfigurationErrorsException(
                    //    string.Format(
                    //        CultureInfo.InvariantCulture, 
                    //        SR.SPECIFIED_BEHAVIOR_CONFIGURATION_ELEMENT_COULD_NOT_BE_LOADED,
                    //        behaviorElementType));

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

        protected static void SetBehavior<TBehavior>(ICollection<TBehavior> collection, BehaviorExtensionElement element)
        {
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
