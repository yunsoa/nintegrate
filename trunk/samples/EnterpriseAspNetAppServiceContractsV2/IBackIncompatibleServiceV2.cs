using System.ServiceModel;

namespace EnterpriseAspNetAppServiceContractsV2
{
    [ServiceContract]
    public interface IBackIncompatibleServiceV2
    {
        BackIncompatibleResultV2 GetIncompatibleResultV2();
    }
}
