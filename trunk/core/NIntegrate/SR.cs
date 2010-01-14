namespace NIntegrate
{
    internal static class SR
    {
        #region Error Messages

        public const string SPECIFIED_SERVICECONTRACT_COULD_NOT_BE_LOADED = "Specified service contract - {0} could not be loaded!";
        public const string MISSING_BINDING_CONFIGURATION_FOR_ENDPOINT = "Missing binding configuration for endpoint: {0}";
        public const string SPECIFIED_BEHAVIOR_CONFIGURATION_ELEMENT_COULD_NOT_BE_LOADED = "Specified behavior configuration element type - {0} could not be loaded!";
        public const string SPECIFIED_BEHAVIOR_CONFIGURATION_ELEMENT_COULD_NOT_BE_INITED = "Specified behavior configuration element type - {0} could not be initialized as BehaviorExtensionElement!";
        public const string FAILED_TO_CREATE_CHANNELFACTORY_FOR_SERVICECONTRACT = "Failed to create ChannelFactory for ServiceContract: {0}";
        public const string SPECIFIED_BINDING_ELEMENT_EXTENSION_CONFIGURATION_ELEMENT_COULD_NOT_BE_INITEDa = "Specified binding element extension element type - {0} could not be initialized as BindingElementExtensionElement!";
        public const string COULD_NOT_LOAD_TYPE = "Could not load type: {0}";
        public const string VALID_MAX_QUEUE_LENGTH = "Max queue length must be -1 (infinite), 0 (process inline) or a positive number.";
        public const string VALID_THREAD_SLEEP_TIME = "Thread sleep interval must be positive.";

        #endregion
    }
}
