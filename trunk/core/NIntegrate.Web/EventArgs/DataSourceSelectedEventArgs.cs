using System.ComponentModel;
using System.Data;

namespace NIntegrate.Web.EventArgs
{
    public class DataSourceSelectedEventArgs : System.EventArgs
    {
        private readonly DataTable _result;

        public DataSourceSelectedEventArgs(DataTable result)
        {
            _result = result;
        }

        public DataTable Result
        {
            get { return _result; }
        }
    }
}
