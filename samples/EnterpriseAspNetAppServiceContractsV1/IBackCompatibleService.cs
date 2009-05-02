using System.Runtime.InteropServices;
using System.ServiceModel;

namespace EnterpriseAspNetAppServiceContracts
{
    [ServiceContract]
    [ComVisible(true)]
    [Guid("F2E27BA1-4E88-4932-82D0-F4B5ECB1535C")]
    public interface IBackCompatibleService
    {
        [OperationContract]
        BackCompatibleResult GetCompatibleResult();
    }
}
