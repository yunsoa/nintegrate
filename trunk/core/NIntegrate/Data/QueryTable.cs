using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using NIntegrate.Data.Configuration;

namespace NIntegrate.Data
{
    /// <summary>
    /// The base class for all query table classes.
    /// 
    /// The QueryTable class is a class represents a table or view in database
    /// for easily querying database with the dynamic query language provided 
    /// by NIntegrate.
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    [KnownType("KnownTypes")]
    public abstract class QueryTable
    {
        [DataMember]
        private readonly string _tableName;
        [DataMember]
        private readonly string _connectionStringName;
        [DataMember]
        private readonly bool _readOnly;
        [DataMember]
        private readonly List<IColumn> _predefinedColumns;

        #region KnownTypes

        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryTable"/> class.
        /// </summary>
        /// <param name="tableName">Name of the table or view to query on.</param>
        /// <param name="connectionStringName">
        ///     The connection string name
        ///     used to locate the connecting string for query.
        /// </param>
        /// <param name="readOnly">If the query table is readonly.</param>
        protected QueryTable(string tableName, string connectionStringName, bool readOnly)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException("tableName");
            if (string.IsNullOrEmpty(connectionStringName))
                throw new ArgumentNullException("connectionStringName");

            _tableName = tableName;
            _connectionStringName = connectionStringName;
            _readOnly = readOnly;
            _predefinedColumns = new List<IColumn>();

            ParsePredefinedColumns();
        }

        #endregion

        #region Public Methods

        public QueryCriteria Select(params IColumn[] columns)
        {
            return new QueryCriteria(_tableName, _connectionStringName,
                _readOnly, _predefinedColumns).Select(columns);
        }

        public QueryCriteria Insert(params Assignment[] assignments)
        {
            return new QueryCriteria(_tableName, _connectionStringName,
                _readOnly, _predefinedColumns).Insert(assignments);
        }

        public QueryCriteria Update(params Assignment[] assignments)
        {
            return new QueryCriteria(_tableName, _connectionStringName,
                _readOnly, _predefinedColumns).Update(assignments);
        }

        public QueryCriteria Delete(params Condition[] conditions)
        {
            var criteria = new QueryCriteria(_tableName, _connectionStringName,
                _readOnly, _predefinedColumns).Delete();
            if (conditions != null && conditions.Length > 0)
            {
                foreach (var condition in conditions)
                {
                    criteria.And(condition);
                }
            }

            return criteria;
        }

        #endregion

        #region Non-Public Methods

        private void ParsePredefinedColumns()
        {
            foreach (var field in GetType().GetFields())
            {
                if (!typeof(IColumn).IsAssignableFrom(field.FieldType)) continue;
                var column = (IColumn)field.GetValue(this);
                _predefinedColumns.Add(column);
            }
            foreach (var property in GetType().GetProperties())
            {
                if (!property.CanRead || !typeof(IColumn).IsAssignableFrom(property.PropertyType)) continue;
                var column = (IColumn)property.GetValue(this, null);
                _predefinedColumns.Add(column);
            }
        }

        #endregion
    }
}
