using System;
using System.ServiceModel;
using System.ServiceModel.Activation;

namespace EAppointments.BMS.Services.STS
{
    public class BMSRealmSTSHostFactory : ServiceHostFactoryBase
    {
        public override ServiceHostBase CreateServiceHost(string constructorString, Uri[] baseAddresses)
        {
            return new BMSRealmSTSHost(baseAddresses);
        }
    }

    class BMSRealmSTSHost : ServiceHost
    {
        public BMSRealmSTSHost(params Uri[] addresses)
            : base(typeof(BMSRealmSTS), addresses)
        {
            ServiceConstants.LoadAppSettings();
        }
    }
}
