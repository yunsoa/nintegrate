using System;

namespace NIntegrate
{
    /// <summary>
    /// The interface for ServiceLocators.
    /// </summary>
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

        /// <summary>
        /// Determines whether the specified service contract is singleton.
        /// </summary>
        /// <param name="serviceContract">The service contract.</param>
        /// <returns>
        /// 	<c>true</c> if the specified service contract is singleton; otherwise, <c>false</c>.
        /// </returns>
        bool IsSingleton(Type serviceContract);
    }
}
