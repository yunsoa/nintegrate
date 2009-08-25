using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class ServiceContractCompatibilityCriteria : CriteriaBase
    {
        public ServiceContractCompatibilityCriteria()
            : base("ServiceContractCompatibility")
        {
        }

        public SqlStringColumn ServiceContract = new SqlStringColumn("ServiceContract", false);
        public SqlStringColumn CompatibleServiceContract = new SqlStringColumn("CompatibleServiceContract", false);
    }
}
