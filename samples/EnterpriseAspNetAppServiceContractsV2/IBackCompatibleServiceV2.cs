using System.Runtime.InteropServices;
using System.ServiceModel;

namespace EnterpriseAspNetAppServiceContractsV2
{
    [ServiceContract]
    [ComVisible(true)]
    [Guid("FDFC1944-5E3A-4fa3-A749-CD1B64FDD08E")]
    public interface IBackCompatibleServiceV2
    {
        [OperationContract]
        BackCompatibleResultV2 GetCompatibleResult();

        [OperationContract]
        string SayHello();
    }
}
