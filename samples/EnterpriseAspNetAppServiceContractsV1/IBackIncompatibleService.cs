using System.ServiceModel;

namespace EnterpriseAspNetAppServiceContractsV1
{
    [ServiceContract]
    public interface IBackIncompatibleService
    {
        BackIncompatibleResult GetResult();
    }
}
