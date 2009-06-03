using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NIntegrate.Test.TestClasses
{
    public class TestServiceLocator : IServiceLocator
    {
        #region IServiceLocator Members

        public object GetService(Type serviceContract)
        {
            if (serviceContract == typeof(IExternalService1))
            {
                return new ExternalService1Impl();
            }
            if (serviceContract == typeof(IExternalService2))
            {
                return new ExternalService2Impl();
            }
            return null;
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public bool IsSingleton(Type serviceContract)
        {
            return false;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
