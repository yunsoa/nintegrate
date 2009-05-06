using System.Collections;
using System.ComponentModel;

namespace NIntegrate.Web.EventArgs
{
    /// <summary>
    /// The InsertingEventArgs for DataSource
    /// </summary>
    public class DataSourceInsertingEventArgs : CancelEventArgs
    {
        private readonly IDictionary _newValues;

		/// <summary>
		/// Initializes a new instance of <see cref="DataSourceInsertingEventArgs"/>.
		/// </summary>
		/// <param name="newValues">An <see cref="IDictionary"/> object with the values being inserted.</param>
        public DataSourceInsertingEventArgs(IDictionary newValues)
        {
            _newValues = newValues;
        }

		/// <summary>
		/// Gets an <see cref="IDictionary"/> object with the values being inserted.
		/// </summary>
        public IDictionary NewValues
        {
            get { return _newValues; }
        }

    }
}
