using System.Data;

namespace NIntegrate.Web.EventArgs
{
    /// <summary>
    /// The SelectedEventArgs for DataSource
    /// </summary>
    public class DataSourceSelectedEventArgs : System.EventArgs
    {
        private readonly DataTable _result;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataSourceSelectedEventArgs"/> class.
        /// </summary>
        /// <param name="result">The result.</param>
        public DataSourceSelectedEventArgs(DataTable result)
        {
            _result = result;
        }

        /// <summary>
        /// Gets the result.
        /// </summary>
        /// <value>The result.</value>
        public DataTable Result
        {
            get { return _result; }
        }
    }
}
