using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class EnvironmentCriteria : CriteriaBase
    {
        public EnvironmentCriteria()
            : base("Environment")
        {
        }

        public SqlInt32Column Environment_id = new SqlInt32Column("Environment_id");
        public SqlStringColumn Name = new SqlStringColumn("Name", false);
        public SqlStringColumn Description = new SqlStringColumn("Description", false);
    }
}
