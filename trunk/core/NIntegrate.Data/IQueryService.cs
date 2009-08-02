using System.ServiceModel;
using System.Data;

namespace NIntegrate.Data
{
    [ServiceContract]
    public interface IQueryService
    {
        [OperationContract]
        DataTable Query(QueryCriteria criteria);

        [OperationContract]
        int Execute(QueryCriteria criteria, bool isCountQuery);
    }
}
