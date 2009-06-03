using System;
using System.ServiceModel;

namespace NIntegrate.Test.TestClasses
{
    [ServiceContract]
    public interface ITestService
    {
        [OperationContract]
        string NormalOperation(string str);

        [OperationContract(IsOneWay = true)]
        void OneWayOperation();

        [OperationContract(AsyncPattern = true)]
        IAsyncResult BeginAsyncOperation(int a, int b, AsyncCallback cb, object state);

        int EndAsyncOperation(IAsyncResult r);

        [OperationContract]
        int AsyncOperation(int a, int b);

        int TransactionOperation();
    }
}
