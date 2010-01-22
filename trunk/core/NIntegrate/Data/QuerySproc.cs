using System;
using System.Runtime.Serialization;

namespace NIntegrate.Data
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public abstract class QuerySproc
    {
        [DataMember]
        private readonly string _sprocName;

        [DataMember]
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

        #region Public Properties

        public string SprocName
        {
            get { return _sprocName; }
        }

        public string ConnectionStringName
        {
            get { return _connectionStringName; }
        }

        #endregion

        #region Public Methods

        public QueryCriteria CreateSprocCriteria(params ParameterEqualsCondition[] parameterConditions)
        {
            return new QueryCriteria(SprocName, ConnectionStringName, false, null).Sproc(parameterConditions);
        }

        #endregion
    }
}
