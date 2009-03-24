namespace NIntegrate.Configuration
{
    public interface IAppVariableProvider
    {
        string GetAppVariable(string appVariableName, string appCode);
    }
}