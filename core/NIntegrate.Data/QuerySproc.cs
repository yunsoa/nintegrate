using System;

namespace NIntegrate.Data
{
    public abstract class QuerySproc
    {
        private readonly string _sprocName;
        private readonly string _connectionStringName;

        #region Constructors

        protected QuerySproc(string sprocName, string connectionStringName)
        {
            if (string.IsNullOrEmpty(sprocName))
                throw new ArgumentNullException("sprocName");
            if (string.IsNullOrEmpty(connectionStringName))
                throw new ArgumentNullException("connectionStringName");

            _sprocName = sprocName;
            _connectionStringName = connectionStringName;
        }

        #endregion

        #region Properties

        public string SprocName
        {
            get { return _sprocName; }
        }

        public string ConnectionStringName
        {
            get { return _connectionStringName; }
        }

        #endregion
    }
}
