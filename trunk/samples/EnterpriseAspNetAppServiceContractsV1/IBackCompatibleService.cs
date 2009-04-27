using System.ServiceModel;

namespace EnterpriseAspNetAppServiceContractsV1
{
    [ServiceContract]
    public interface IBackCompatibleService
    {
        BackCompatibleResult GetResult();
    }
}
