using System;
using System.Data;

namespace NIntegrate.Data
{
    public sealed class QueryService : IQueryService
    {
        #region IQueryService Members

        public DataTable Query(QueryCriteria criteria)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
