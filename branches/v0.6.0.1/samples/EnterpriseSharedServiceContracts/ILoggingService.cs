using System.ServiceModel;

namespace EnterpriseSharedServiceContracts
{
    [ServiceContract(Namespace="http://nintegrate.com/Samples")]
    public interface ILoggingService
    {
        [OperationContract(IsOneWay = true)]
        void WriteLog(string message);
    }
}
