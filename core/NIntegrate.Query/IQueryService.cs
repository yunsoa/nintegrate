using System.ServiceModel;
using System.Data;

namespace NIntegrate.Query
{
    [ServiceContract]
    public interface IQueryService
    {
        [OperationContract]
        DataTable Select(Criteria criteria);
        int SelectCount(Criteria criteria);
        int Update(Criteria criteria, DataTable modifiedTable);
    }
}
