using System.ServiceModel;

namespace SimpleServiceContracts
{
    [ServiceContract]
    public interface ISimpleServiceDemo
    {
        [OperationContract]
        string SayHellod();
    }
}
