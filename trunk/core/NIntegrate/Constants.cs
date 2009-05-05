using System.Configuration;

namespace NIntegrate
{
    /// <summary>
    /// Constants used by NIntegrate framework.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Initializes the <see cref="Constants"/> class.
        /// </summary>
        static Constants()
        {
            DefaultServiceExtension = ConfigurationManager.AppSettings[DefaultServiceExtensionAppSettingName] ?? ".svc";
        }

        /// <summary>
        /// The default extension for WCF service deploy file. 
        /// </summary>
        public static readonly string DefaultServiceExtension;

        /// <summary>
        /// The app setting name for specifying default extension for WCF service deploy file. 
        /// </summary>
        public static readonly string DefaultServiceExtensionAppSettingName = "NIntegrate.Configuration.DefaultServiceExtension";

        /// <summary>
        /// The connection string name for config db 
        /// </summary>
        public const string ConfigurationDatabaseConnectionStringName = "NIntegrate.Configuration";

        /// <summary>
        /// The appSetting name for specifying connectionstring provider type
        /// </summary>
        public const string ConnectionStringProviderTypeAppSettingName = "NIntegrate.Configuration.ConnectionStringProvider";

        /// <summary>
        /// The appSetting name for specifying service configuration provider type
        /// </summary>
        public const string ServiceConfigurationProviderTypeAppSettingName = "NIntegrate.Configuration.ServiceConfigurationProvider";

        /// <summary>
        /// The appSetting name for specifying service deployment configuration provider type
        /// </summary>
        public const string ServiceDeploymentConfigurationProviderTypeAppSettingName = "NIntegrate.Configuration.ServiceDeploymentConfigurationProvider";

        /// <summary>
        /// The appSetting name for specifying app variable provider type
        /// </summary>
        public const string AppVariableProviderTypeAppSettingName = "NIntegrate.Configuration.AppVariableProvider";

        /// <summary>
        /// The appSetting name for specifying app code for getting app variables
        /// </summary>
        public const string AppCodeAppSettingName = "NIntegrate.Configuration.AppCode";

        /// <summary>
        /// The appSetting name for specifying external service locator type
        /// </summary>
        public const string ExternalServiceLocatorTypeAppSettingName = "NIntegrate.ExternalServiceLocator";
    }
}
