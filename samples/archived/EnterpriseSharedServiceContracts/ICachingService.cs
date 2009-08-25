using System.ServiceModel;
using System;

namespace EnterpriseSharedServiceContracts
{
    [ServiceContract]
    public interface ICachingService
    {
        [OperationContract]
        byte[] GetCache(string key);
        [OperationContract(IsOneWay = true)]
        void SetCache(string key, byte[] data, TimeSpan expireTimeSpan);
    }
}
