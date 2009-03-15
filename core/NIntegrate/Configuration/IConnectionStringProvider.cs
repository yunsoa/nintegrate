namespace NIntegrate.Configuration
{
    public interface IConnectionStringProvider
    {
        ConnectionString GetConnectionString(string connectionStringName);
    }
}
