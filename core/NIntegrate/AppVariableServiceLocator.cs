using System;
using NIntegrate.Configuration;

namespace NIntegrate
{
    public sealed class AppVariableServiceLocator : IServiceLocator
    {
        #region IServiceLocator Members

        public object GetService(Type serviceContract)
        {
            if (serviceContract == null)
                throw new ArgumentNullException("serviceContract");
            var serviceImplTypeName = AppVariableStore.GetAppVariable(serviceContract.AssemblyQualifiedName);
            if (!string.IsNullOrEmpty(serviceImplTypeName))
            {
                var serviceImplType = Type.GetType(serviceImplTypeName, true);
                Activator.CreateInstance(serviceImplType);
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
