﻿using System;
using System.ServiceModel.Configuration;
using NIntegrate.ServiceModel.Collections.Generic;

namespace NIntegrate.ServiceModel.Configuration
{
    public sealed class BindingElementExtensionRegistry : Registry<string, Type>
    {
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