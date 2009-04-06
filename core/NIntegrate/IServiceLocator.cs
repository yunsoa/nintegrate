using System;

namespace NIntegrate
{
    public interface IServiceLocator : IDisposable
    {
        /// <summary>
        /// Get service
        /// </summary>
        /// <param name="serviceContract">The service contract type</param>
        /// <returns>The service instance</returns>
        object GetService(Type serviceContract);

        /// <summary>
        /// Generic version of GetService
        /// </summary>
        /// <typeparam name="T">The service contract type</typeparam>
        /// <returns>The service instance</returns>
        T GetService<T>();

        bool IsSingleton(Type serviceContract);
    }
}
