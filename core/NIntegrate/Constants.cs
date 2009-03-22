namespace NIntegrate
{
    public static class Constants
    {
        /// <summary>
        /// The connection string name for config db 
        /// </summary>
        public const string ConfigurationDatabaseConnectionStringName = "NIntegrate.Configuration";

        /// <summary>
        /// The appSetting name for specifying connectionstring provider type
        /// </summary>
        public const string ConnectionStringProviderTypeAppSettingName = "NIntegrate.Configuration.ConnectionStringProvider";

        /// <summary>
        /// The appSetting name for specifying endpoint provider type
        /// </summary>
        public const string EndpointProviderTypeAppSettingName = "NIntegrate.Configuration.EndpointProvider";

        /// <summary>
        /// The appSetting name for specifying external service locator type
        /// </summary>
        public const string ExternalServiceLocatorTypeAppSettingName = "NIntegrate.ExternalServiceLocator";
    }
}
