using System;
using System.ServiceModel.Configuration;
using NIntegrate.Collections.Generic;

namespace NIntegrate.ServiceModel.Configuration
{
    /// <summary>
    /// The BehaviorExtensionRegistry class is a registry for any custom behavior extensions. It maps to the &lt;behaviorExtensions&gt; configuratin element in &lt;system.serviceModel&gt; section.
    /// </summary>
    public sealed class BehaviorExtensionRegistry : Registry<string, Type>
    {
        public static readonly BehaviorExtensionRegistry Instance;

        #region Constructors

        private BehaviorExtensionRegistry()
        {
            LoadBuildInBehaviorTypes();
        }

        static BehaviorExtensionRegistry()
        {
            Instance = new BehaviorExtensionRegistry();
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

        private void LoadBuildInBehaviorTypes()
        {
            AddItem("persistenceprovider", typeof(PersistenceProviderElement));
            AddItem("workflowruntime", typeof(WorkflowRuntimeElement));
            AddItem("enablewebscript", typeof(WebScriptEnablingElement));
            AddItem("webHttp", typeof(WebHttpElement));
            AddItem("webHttp", typeof(WebHttpElement));
            AddItem("callbackTimeOuts", typeof(CallbackTimeoutsElement));
            AddItem("serviceTimeOuts", typeof(ServiceTimeoutsElement));
        }

        #endregion
    }
}
