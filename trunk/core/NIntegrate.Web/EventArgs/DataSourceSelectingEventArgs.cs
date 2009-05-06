using System.ComponentModel;
using System.Collections.Specialized;
using NIntegrate.Query;
using System.Data;

namespace NIntegrate.Web.EventArgs
{
    /// <summary>
    /// The SelectingEventArgs for DataSource
    /// </summary>
    public class DataSourceSelectingEventArgs : CancelEventArgs
    {
        private readonly Criteria _criteria;
        private readonly IOrderedDictionary _parameters;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSourceSelectingEventArgs"/> class.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <param name="parameters">The parameters.</param>
        public DataSourceSelectingEventArgs(Criteria criteria, IOrderedDictionary parameters)
        {
            _criteria = criteria;
            _parameters = parameters;
        }

        /// <summary>
        /// Gets the criteria.
        /// </summary>
        /// <value>The criteria.</value>
        public Criteria Criteria
        {
            get { return _criteria; }
        }

        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <value>The parameters.</value>
        public IOrderedDictionary Parameters
        {
            get { return _parameters; }
        }

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>The result.</value>
        public DataTable Result { get; set; }
    }
}
