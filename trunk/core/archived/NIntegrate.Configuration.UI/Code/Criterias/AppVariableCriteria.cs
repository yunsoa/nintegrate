using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class AppVariableCriteria : CriteriaBase
    {
        public AppVariableCriteria() : base("AppVariable")
        {
        }

        public SqlStringColumn AppVariableName = new SqlStringColumn("AppVariableName", false);
        public SqlStringColumn AppCode = new SqlStringColumn("AppCode", false);
        public SqlInt32Column Environment_id = new SqlInt32Column("Environment_id");
        public SqlStringColumn Value = new SqlStringColumn("Value", false);
        public SqlStringColumn Description = new SqlStringColumn("Description", false);
    }
}
