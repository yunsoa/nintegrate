using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace NIntegrate.Configuration
{
    /// <summary>
    /// The LINQ DataContext for all stored procedures.
    /// </summary>
    partial class StoredProceduresDataContext
    {
        /// <summary>
        /// The get service configuration stored procedure.
        /// </summary>
        /// <param name="serviceName">Name of the service.</param>
        /// <param name="serverName">Name of the server.</param>
        /// <param name="appCode">The app code.</param>
        /// <returns></returns>
        [Function(Name = "dbo.sp_GetServiceConfiguration")]
        [ResultType(typeof(ServiceConfiguration))]
        [ResultType(typeof(EndpointConfiguration))]
        public IMultipleResults sp_GetServiceConfiguration([Parameter(Name = "ServiceName", DbType = "VarChar(255)")] string serviceName, [Parameter(Name = "ServerName", DbType = "VarChar(50)")] string serverName, [Parameter(Name = "AppCode", DbType = "VarChar(10)")] string appCode)
        {
            var result = ExecuteMethodCall(this, ((MethodInfo)(MethodBase.GetCurrentMethod())), serviceName, serverName, appCode);
            return (IMultipleResults)(result.ReturnValue);
        }
    }
}
