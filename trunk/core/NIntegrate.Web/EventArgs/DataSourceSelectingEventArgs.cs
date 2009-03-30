using System.ComponentModel;
using System.Collections.Specialized;
using NIntegrate.Query;

namespace NIntegrate.Web.EventArgs
{
    public class DataSourceSelectingEventArgs : CancelEventArgs
    {
        private readonly Criteria _criteria;
        private readonly IOrderedDictionary _parameters;
        
        public DataSourceSelectingEventArgs(Criteria criteria, IOrderedDictionary parameters)
        {
            _criteria = criteria;
            _parameters = parameters;
        }

        public Criteria Criteria
        {
            get { return _criteria; }
        }

        public IOrderedDictionary Parameters
        {
            get { return _parameters; }
        }
    }
}
