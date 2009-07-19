using System.ServiceModel;

namespace NIntegrate.Test.TestClasses
{
    [ServiceBehavior(
        ConcurrencyMode = ConcurrencyMode.Single,
        InstanceContextMode = InstanceContextMode.PerSession
    )]
    public class TestServiceImpl : ITestService, ITestService2
    {
        #region ITestService Members

        public string NormalOperation(string str)
        {
            return str;
        }

        public void OneWayOperation()
        {
        }

        public System.IAsyncResult BeginAsyncOperation(int a, int b, System.AsyncCallback cb, object state)
        {
            return null;
        }

        public int EndAsyncOperation(System.IAsyncResult r)
        {
            return 0;
        }

        public int AsyncOperation(int a, int b)
        {
            return 0;
        }

        [OperationBehavior(TransactionAutoComplete = true,
            TransactionScopeRequired = true
        )]
        public int TransactionOperation()
        {
            return 0;
        }

        #endregion

        #region ITestService2 Members

        public string Hello2()
        {
            return "hello";
        }

        #endregion
    }
}
