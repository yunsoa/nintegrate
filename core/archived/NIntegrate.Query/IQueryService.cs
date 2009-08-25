using System.ServiceModel;
using System.Data;

namespace NIntegrate.Query
{
    [ServiceContract(Namespace = "http://nintegrate.com/Query")]
    public interface IQueryService
    {
        [OperationContract]
        DataTable Select(Criteria criteria);
        [OperationContract]
        int SelectCount(Criteria criteria);
        [OperationContract]
        int Update(Criteria criteria, DataTable modifiedTable, ConflictOption conflictDetection);
    }
}
