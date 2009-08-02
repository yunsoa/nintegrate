using System;
using System.Collections.Generic;

namespace NIntegrate.Data
{
    /// <summary>
    /// The base class for all query table classes.
    /// 
    /// A query table class is a class represents a table or view in database
    /// for easily query database with the dynamic query language provided 
    /// by NIntegrate.
    /// </summary>
    public abstract class QueryTable
    {
        private readonly string _tableName;
        private readonly string _connectionStringName;
        private readonly bool _readOnly;
        private readonly List<IColumn> _predefinedColumns;

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

        /// <summary>
        /// Create a new query criteria from the query table.
        /// </summary>
        /// <returns>A default query criteria, which select * from the table.</returns>
        public QueryCriteria CreateCriteria()
        {
            return new QueryCriteria(_tableName, _connectionStringName, _readOnly, _predefinedColumns);
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
