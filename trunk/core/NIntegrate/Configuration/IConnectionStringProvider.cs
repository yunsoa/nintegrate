using System.ServiceModel;
namespace NIntegrate.Configuration
{
    [ServiceContract]
    public interface IConnectionStringProvider
    {
        [OperationContract]
        ConnectionString GetConnectionString(string connectionStringName);
    }
}
