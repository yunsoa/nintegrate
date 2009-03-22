using System.ServiceModel;
using System.Data;

namespace NIntegrate.Query
{
    [ServiceContract]
    public interface IQueryService
    {
        [OperationContract]
        DataTable Select(Criteria criteria);
        [OperationContract]
        int SelectCount(Criteria criteria);
        [OperationContract]
        int Update(Criteria criteria, DataTable modifiedTable);
    }
}
