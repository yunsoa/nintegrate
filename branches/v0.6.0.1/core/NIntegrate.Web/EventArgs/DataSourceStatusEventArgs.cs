namespace NIntegrate.Web.EventArgs
{
    public class DataSourceStatusEventArgs : System.EventArgs
    {
        private readonly int _affectedRows;
        private readonly object _instance;

		/// <summary>
		/// Initializes a new instance of <see cref="DataSourceStatusEventArgs"/>.
		/// </summary>
		/// <param name="instance">The object instance affected by the data operation.</param>
		/// <param name="affectedRows">The number of rows that are affected by the data operation.</param>
        public DataSourceStatusEventArgs(object instance, int affectedRows)
        {
            _affectedRows = affectedRows;
            _instance = instance;
        }
		/// <summary>
		/// Gets the number of rows that are affected by the data operation.
		/// </summary>
        public int AffectedRows
        {
            get { return _affectedRows; }
        }

		/// <summary>
		/// Gets the object instance affected by the data operation.
		/// </summary>
        public object Instance
        {
            get { return _instance; }
        } 
    }
}
