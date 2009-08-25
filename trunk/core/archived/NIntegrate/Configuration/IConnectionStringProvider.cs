using System.ServiceModel;
namespace NIntegrate.Configuration
{
    /// <summary>
    /// The interface for ConnectionStringProviders.
    /// </summary>
    [ServiceContract]
    public interface IConnectionStringProvider
    {
        /// <summary>
        /// Get connection string by given connectionStringName.
        /// </summary>
        /// <param name="connectionStringName">Name of the connection string.</param>
        /// <returns></returns>
        [OperationContract]
        ConnectionString GetConnectionString(string connectionStringName);
    }
}
