using System.Collections;
using System.ComponentModel;

namespace NIntegrate.Web.EventArgs
{
    public class DataSourceDeletingEventArgs : CancelEventArgs
    {
        private readonly IDictionary _keys;
        private readonly IDictionary _oldValues;

		/// <summary>
		/// Initializes a new instance of <see cref="DataSourceDeletingEventArgs"/>.
		/// </summary>
		/// <param name="keys">An <see cref="IDictionary"/> object with the affected keys.</param>
		/// <param name="oldValues">An <see cref="IDictionary"/> object with the old values.</param>
        public DataSourceDeletingEventArgs(IDictionary keys, IDictionary oldValues)
        {
            _keys = keys;
            _oldValues = oldValues;
        }

		/// <summary>
		/// Gets the affected keys <see cref="IDictionary"/>.
		/// </summary>
        public IDictionary Keys
        {
            get { return _keys; }
        }

		/// <summary>
		/// Gets the old values <see cref="IDictionary"/>.
		/// </summary>
        public IDictionary OldValues
        {
            get { return _oldValues; }
        }
    }
}
