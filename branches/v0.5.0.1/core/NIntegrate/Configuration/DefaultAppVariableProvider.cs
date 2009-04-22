using System;
using System.Configuration;
using System.Data.SqlClient;

namespace NIntegrate.Configuration
{
    public class DefaultAppVariableProvider : IAppVariableProvider
    {
        #region IAppVariableProvider Members

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
