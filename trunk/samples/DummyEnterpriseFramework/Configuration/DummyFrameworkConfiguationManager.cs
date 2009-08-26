namespace DummyEnterpriseFramework.Configuration
{
    public static class DummyFrameworkConfiguationManager
    {
        public static IDummyFrameworkConfiguation GetConfiguration()
        {
            //load the config from somewhere
            var config = new DummyFrameworkConfiguation();
            return config;
        }
    }
}
