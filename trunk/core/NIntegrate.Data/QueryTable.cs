using System;
using System.Collections.Generic;

namespace NIntegrate.Data
{
    public abstract class QueryTable
    {
        private readonly string _tableName;
        private readonly string _connectionStringName;
        private readonly bool _isReadonly;
        private readonly List<IColumn> _predefinedColumns = new List<IColumn>();

        #region Constructors

        protected QueryTable(string tableName, string connectionStringName, bool isReadonly)
        {
            if (string.IsNullOrEmpty(tableName))
                throw new ArgumentNullException("tableName");
            if (string.IsNullOrEmpty(connectionStringName))
                throw new ArgumentNullException("connectionStringName");

            _tableName = tableName;
            _connectionStringName = connectionStringName;
            _isReadonly = isReadonly;

            ParsePredefinedColumns();
        }

        #endregion

        #region Public Methods

        public QueryCriteria CreateCriteria()
        {
            return new QueryCriteria(_tableName, _connectionStringName, _isReadonly, _predefinedColumns);
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
