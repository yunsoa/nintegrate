using System;
using NIntegrate.Configuration;

namespace NIntegrate
{
    /// <summary>
    /// A build-in IServiceLocator implementation which use AppVariables 
    /// to map service contract type to service implementation type.
    /// </summary>
    public sealed class AppVariableServiceLocator : IServiceLocator
    {
        #region IServiceLocator Members

        /// <summary>
        /// Get service
        /// </summary>
        /// <param name="serviceContract">The service contract type</param>
        /// <returns>The service instance</returns>
        public object GetService(Type serviceContract)
        {
            if (serviceContract == null)
                throw new ArgumentNullException("serviceContract");
            var serviceImplTypeName = AppVariableStore.GetAppVariable(serviceContract.GetQualifiedTypeName());
            if (!string.IsNullOrEmpty(serviceImplTypeName))
            {
                var serviceImplType = Type.GetType(serviceImplTypeName, true);
                return Activator.CreateInstance(serviceImplType);
            }

            return null;
        }

        /// <summary>
        /// Generic version of GetService
        /// </summary>
        /// <typeparam name="T">The service contract type</typeparam>
        /// <returns>The service instance</returns>
        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        /// <summary>
        /// Determines whether the specified service contract is singleton.
        /// </summary>
        /// <param name="serviceContract">The service contract.</param>
        /// <returns>
        /// 	<c>true</c> if the specified service contract is singleton; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSingleton(Type serviceContract)
        {
            return false;
        }

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
        }

        #endregion
    }
}
