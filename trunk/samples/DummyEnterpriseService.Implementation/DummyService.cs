using System;
using DummyEnterpriseService.Interface;

namespace DummyEnterpriseService.Implementation
{
    public class DummyService : IDummyService
    {
        #region IDummyService Members

        public string SayHello()
        {
            return "Hello " + Environment.MachineName;
        }

        #endregion
    }
}
