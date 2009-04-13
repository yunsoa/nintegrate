using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Reflection;

namespace NIntegrate.Configuration
{
    partial class StoredProceduresDataContext
    {
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
