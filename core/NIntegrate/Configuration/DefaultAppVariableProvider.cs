using System;
using System.Data.SqlClient;

namespace NIntegrate.Configuration
{
    /// <summary>
    /// The build-in IAppVariableProvider implementation,.
    /// </summary>
    public class DefaultAppVariableProvider : IAppVariableProvider
    {
        #region IAppVariableProvider Members

        /// <summary>
        /// Gets AppVariable value by given appVariableName and appCode.
        /// </summary>
        /// <param name="appVariableName">Name of the app variable.</param>
        /// <param name="appCode">The app code.</param>
        /// <returns></returns>
        public string GetAppVariable(string appVariableName, string appCode)
        {
            if (string.IsNullOrEmpty(appVariableName))
                throw new ArgumentNullException("appVariableName");

            string retValue = null;

            using (var conn = new SqlConnection(WcfServiceHelper.GetConfigurationConnectionString()))
            {
                conn.Open();

                var context = new StoredProceduresDataContext(conn);
                var results = context.sp_GetAppVariable(appVariableName, appCode,
                    Environment.MachineName.ToLowerInvariant());

                foreach (var result in results)
                {
                    retValue = result.Value;
                    break;
                }

                conn.Close();
                conn.Dispose();
            }

            return retValue;
        }

        #endregion
    }
}
