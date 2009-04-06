using System.ServiceModel;
namespace NIntegrate.Configuration
{
    [ServiceContract]
    public interface IAppVariableProvider
    {
        [OperationContract]
        string GetAppVariable(string appVariableName, string appCode);
    }
}