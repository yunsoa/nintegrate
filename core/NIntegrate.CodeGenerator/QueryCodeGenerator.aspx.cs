using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NIntegrate.Data;
using System.Configuration;
using NIntegrate.Web;
using NIntegrate.Data.SqlClient;
using System.Data;

namespace NIntegrate.CodeGenerator
{
    public partial class QueryCodeGenerator : System.Web.UI.Page
    {
        private DataView Query(QueryCriteria criteria)
        {
            var queryService = new QueryService(new CodeGeneratorQueryCommandFactory(tbConnStr.Text));
            return new DataView(queryService.Query(criteria));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            tbOutputDir.Text = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        }

        protected void btnConnect_Click(object sender, EventArgs e)
        {
            var sysObjs = new sysobjects();
            cblTables.DataSource = Query(sysObjs.Select(sysObjs.name).And(sysObjs.xtype == "U" && sysObjs.name != "sysdiagrams").SortBy(sysObjs.name, false));
            cblViews.DataSource = Query(sysObjs.Select(sysObjs.name).And(sysObjs.xtype == "V").SortBy(sysObjs.name, false));
            cblSprocs.DataSource = Query(sysObjs.Select(sysObjs.name).And(sysObjs.xtype == "P" && !sysObjs.name.StartsWith("dt_") && !sysObjs.name.Like("sp_%diagram%")).SortBy(sysObjs.name, false));

            DataBind();
        }
    }

    #region Query Classes

    public class CodeGeneratorQueryCommandFactory : QueryCommandFactory
    {
        private readonly ConnectionStringSettings _connStr;

        public CodeGeneratorQueryCommandFactory(string connStr)
        {
            _connStr = new ConnectionStringSettings("Default", connStr);
        }

        protected override System.Configuration.ConnectionStringSettings GetConnectionString(string connectionStringName)
        {
            return _connStr;
        }
    }

    public class sysobjects : QueryTable
    {
        public sysobjects()
            : base("sysobjects", "Default", true)
        {
        }

        public StringColumn name = new StringColumn("name", true);
        public StringColumn xtype = new StringColumn("xtype", false);
    }

    #endregion
}
