using System;
using System.Collections.Generic;
using System.Configuration;

namespace NIntegrate.Configuration
{
    public sealed class AppVariableStore
    {
        #region Private Singleton

        private static readonly AppVariableStore _singleton = new AppVariableStore();

        #endregion

        #region Private Constructor

        private readonly IAppVariableProvider _provider;

        private AppVariableStore()
        {
            var providerTypeName = ConfigurationManager.AppSettings[Constants.AppVariableProviderTypeAppSettingName];
            if (!string.IsNullOrEmpty(providerTypeName))
            {
                var providerType = Type.GetType(providerTypeName);
                if (providerType != null)
                    _provider = (IAppVariableProvider)Activator.CreateInstance(providerType);
            }

            if (_provider == null)
                _provider = new DefaultAppVariableProvider();
        }

        #endregion

        #region Private App Variable Cache

        private static readonly Dictionary<string, string> _cachedAppVariables
            = new Dictionary<string, string>();

        #endregion

        public static string GetAppVariable(string appVariableName, string appCode)
        {
            if (string.IsNullOrEmpty(appVariableName))
                throw new ArgumentNullException("appVariableName");
            if (string.IsNullOrEmpty(appCode))
                throw new ArgumentNullException("appCode");

            var cacheKey = appCode + '|' + appVariableName;

            if (!_cachedAppVariables.ContainsKey(cacheKey))
            {
                lock (_cachedAppVariables)
                {
                    if (!_cachedAppVariables.ContainsKey(cacheKey))
                    {
                        var retValue = _singleton._provider.GetAppVariable(appVariableName, appCode);
                        _cachedAppVariables.Add(cacheKey, retValue);
                    }
                }
            }

            return _cachedAppVariables[cacheKey];
        }
    }
}
