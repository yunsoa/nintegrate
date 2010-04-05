using System;
using System.Runtime.Serialization;

namespace NIntegrate.Data
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public abstract class QuerySproc
    {
        [DataMember]
        private readonly string _sprocName;

        [DataMember]
        private readonly string _connectionStringName;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QuerySproc"/> class.
        /// </summary>
        /// <param name="sprocName">Name of the sproc.</param>
        /// <param name="connectionStringName">Name of the connection string.</param>
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

        #region Public Properties

        /// <summary>
        /// Gets the name of the sproc.
        /// </summary>
        /// <value>The name of the sproc.</value>
        public string SprocName
        {
            get { return _sprocName; }
        }

        /// <summary>
        /// Gets the name of the connection string.
        /// </summary>
        /// <value>The name of the connection string.</value>
        public string ConnectionStringName
        {
            get { return _connectionStringName; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Creates the sproc criteria.
        /// </summary>
        /// <param name="parameterConditions">The parameter conditions.</param>
        /// <returns></returns>
        public QueryCriteria CreateSprocCriteria(params ParameterEqualsCondition[] parameterConditions)
        {
            return new QueryCriteria(SprocName, ConnectionStringName, false, null).Sproc(parameterConditions);
        }

        #endregion
    }
}
