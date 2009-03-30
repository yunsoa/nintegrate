using System.Collections;
using System.ComponentModel;

namespace NIntegrate.Web.EventArgs
{
    public class DataSourceUpdatingEventArgs : CancelEventArgs
    {
        private readonly IDictionary _keys;
        private readonly IDictionary _oldvalues;
        private readonly IDictionary _newValues;
        
		/// <summary>
		/// Initializes a new instance of <see cref="DataSourceUpdatingEventArgs"/>.
		/// </summary>
		/// <param name="keys">An <see cref="IDictionary"/> with the keys affected by the data operation.</param>
		/// <param name="newValues">An <see cref="IDictionary"/> with the values previous to the data operation.</param>
		/// <param name="oldValues">An <see cref="IDictionary"/> with the values after the data operation.</param>
        public DataSourceUpdatingEventArgs(IDictionary keys, IDictionary newValues, IDictionary oldValues)
        {
            _keys = keys;
            _oldvalues = oldValues;
            _newValues = newValues;
        }

		/// <summary>
		/// Gets an <see cref="IDictionary"/> with the keys affected by the data operation
		/// </summary>
        public IDictionary Keys
        {
            get { return _keys; }
        }

		/// <summary>
		/// Gets an <see cref="IDictionary"/> with the values previous to the data operation.
		/// </summary>
        public IDictionary NewValues
        {
            get { return _newValues; }
        }

		/// <summary>
		/// Gets an <see cref="IDictionary"/> with the values after the data operation.
		/// </summary>
        public IDictionary OldValues
        {
            get { return _oldvalues; }
        }
    }
}
