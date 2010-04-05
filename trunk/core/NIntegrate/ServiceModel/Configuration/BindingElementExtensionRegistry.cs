using System;
using System.ServiceModel.Configuration;
using NIntegrate.Collections.Generic;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// The BindingElementExtensionRegistry class is a registry for any custom binding element extensions. It maps to the &lt;bindingElementExtensions&gt; configuration element in &lt;system.serviceModel&gt; section.
    /// </summary>
    public sealed class BindingElementExtensionRegistry : Registry<string, Type>
    {
        /// <summary>
        /// Singleton
        /// </summary>
        public static readonly BindingElementExtensionRegistry Instance;

        #region Constructors

        private BindingElementExtensionRegistry()
        {
            LoadBuildInBindingElementTypes();
        }

        static BindingElementExtensionRegistry()
        {
            Instance = new BindingElementExtensionRegistry();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the <see cref="System.Type"/> with the specified key.
        /// </summary>
        /// <value></value>
        public override Type this[string key]
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

        /// <summary>
        /// Adds an item.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public override bool AddItem(string key, Type value)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("key");
            if (value == null)
                throw new ArgumentNullException("value");

            key = key.ToLowerInvariant();

            return base.AddItem(key, value);
        }

        #endregion

        #region Non-Public Methods

        private void LoadBuildInBindingElementTypes()
        {
            AddItem("webmessageencoding", typeof(WebMessageEncodingElement));
            AddItem("context", typeof(ContextBindingElementExtensionElement));
        }

        #endregion
    }
}