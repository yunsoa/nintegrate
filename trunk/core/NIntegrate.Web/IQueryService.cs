using System.ServiceModel;
using System.Data;
using NIntegrate.Data;

namespace NIntegrate.Web
{
    /// <summary>
    /// A build-in simple query service contract used by QueryDataCourse control.
    /// </summary>
    [ServiceContract]
    public interface IQueryService
    {
        [OperationContract]
        DataTable Query(QueryCriteria criteria);

        [OperationContract]
        int Execute(QueryCriteria criteria, bool isCountQuery);
    }
}
