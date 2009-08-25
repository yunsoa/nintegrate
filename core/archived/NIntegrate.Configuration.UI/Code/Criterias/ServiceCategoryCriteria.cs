using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class ServiceCategoryCriteria : CriteriaBase
    {
        public ServiceCategoryCriteria()
            : base("ServiceCategory")
        {
        }

        public SqlInt32Column ServiceCategory_id = new SqlInt32Column("ServiceCategory_id");
        public SqlStringColumn ServiceCategoryName = new SqlStringColumn("ServiceCategoryName", false);
    }
}
