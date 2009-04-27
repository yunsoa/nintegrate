using System.ServiceModel;

namespace EnterpriseAspNetAppServiceContractsV2
{
    [ServiceContract]
    public interface IBackCompatibleServiceV2
    {
        BackCompatibleResultV2 GetResult();

        string SayHello();
    }
}
