using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NIntegrate.Query.SqlClient;

namespace NIntegrate.Configuration.UI.Code.Criterias
{
    public sealed class AppCriteria : CriteriaBase
    {
        public AppCriteria() : base("App")
        {
        }

        public SqlStringColumn AppCode = new SqlStringColumn("AppCode", false);
        public SqlStringColumn Name = new SqlStringColumn("Name", false);
        public SqlStringColumn Description = new SqlStringColumn("Description", false);
    }
}
