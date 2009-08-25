using System.ServiceModel;
namespace NIntegrate.Configuration
{
    /// <summary>
    /// The interface for AppVariableProviders.
    /// </summary>
    [ServiceContract]
    public interface IAppVariableProvider
    {
        /// <summary>
        /// Gets AppVariable value by given appVariableName and appCode.
        /// </summary>
        /// <param name="appVariableName">Name of the app variable.</param>
        /// <param name="appCode">The app code.</param>
        /// <returns></returns>
        [OperationContract]
        string GetAppVariable(string appVariableName, string appCode);
    }
}