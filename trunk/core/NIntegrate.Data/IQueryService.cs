using System.ServiceModel;
using System.Data;

namespace NIntegrate.Data
{
    [ServiceContract]
    public interface IQueryService
    {
        DataTable Query(QueryCriteria criteria);
        int Execute(QueryCriteria criteria, bool isCountQuery);
    }
}
