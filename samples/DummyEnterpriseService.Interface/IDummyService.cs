using System.ServiceModel;

namespace DummyEnterpriseService.Interface
{
    [ServiceContract]
    public interface IDummyService
    {
        [OperationContract]
        string SayHello();
    }
}
