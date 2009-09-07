using System;
using System.Data.Common;
using System.Data;

namespace NIntegrate.Data
{
    public sealed class SprocDbCommand : DbCommand, IDisposable
    {
        private readonly DbCommand _cmd;
        private readonly QueryCommandBuilder _builder;

        #region Constructors

        internal SprocDbCommand(DbCommand cmd, QueryCommandBuilder builder)
        {
            _cmd = cmd;
            _builder = builder;
        }

        #endregion

        #region DbCommand Members

        public override void Cancel()
        {
            _cmd.Cancel();
        }

        public override string CommandText
        {
            get
            {
                return _cmd.CommandText;
            }
            set
            {
                _cmd.CommandText = value;
            }
        }

        public override int CommandTimeout
        {
            get
            {
                return _cmd.CommandTimeout;
            }
            set
            {
                _cmd.CommandTimeout = value;
            }
        }

        public override CommandType CommandType
        {
            get
            {
                return _cmd.CommandType;
            }
            set
            {
                _cmd.CommandType = value;
            }
        }

        protected override DbParameter CreateDbParameter()
        {
            return _cmd.CreateParameter();
        }

        protected override DbConnection DbConnection
        {
            get
            {
                return _cmd.Connection;
            }
            set
            {
                _cmd.Connection = value;
            }
        }

        protected override DbParameterCollection DbParameterCollection
        {
            get { return _cmd.Parameters; }
        }

        protected override DbTransaction DbTransaction
        {
            get
            {
                return _cmd.Transaction;
            }
            set
            {
                _cmd.Transaction = value;
            }
        }

        public override bool DesignTimeVisible
        {
            get
            {
                return _cmd.DesignTimeVisible;
            }
            set
            {
                _cmd.DesignTimeVisible = value;
            }
        }

        protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
        {
            var result = _cmd.ExecuteReader();
            return result;
        }

        public override int ExecuteNonQuery()
        {
            var result = _cmd.ExecuteNonQuery();
            return result;
        }

        public override object ExecuteScalar()
        {
            var result = _cmd.ExecuteScalar();
            return result;
        }

        public override void Prepare()
        {
            _cmd.Prepare();
        }

        public override UpdateRowSource UpdatedRowSource
        {
            get
            {
                return _cmd.UpdatedRowSource;
            }
            set
            {
                _cmd.UpdatedRowSource = value;
            }
        }

        #endregion

        #region IDisposable Members

        void IDisposable.Dispose()
        {
            _cmd.Dispose();
        }

        #endregion

        #region Public Methods

        public SprocDbCommand AddParameter(ParameterEqualsCondition sprocParamCondition)
        {
            if (ReferenceEquals(sprocParamCondition, null))
                throw new ArgumentNullException("sprocParamCondition");

            _builder.AddParameterEqualsConditionParameter(sprocParamCondition, _cmd.Parameters);

            return this;
        }

        public bool? GetOutputParameterValue(BooleanParameterExpression sprocParam)
        {
            if (ReferenceEquals(sprocParam, null))
                throw new ArgumentNullException("sprocParam");

            foreach (DbParameter p in _cmd.Parameters)
            {
                if (p.Direction != ParameterDirection.Input
                    && string.Compare(
                       p.ParameterName,
                       _builder.ToParameterName(sprocParam.ID),
                       StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (p.Value == DBNull.Value)
                        return null;
                    return (bool)p.Value;
                }
            }

            return null;
        }

        public byte? GetOutputParameterValue(ByteParameterExpression sprocParam)
        {
            if (ReferenceEquals(sprocParam, null))
                throw new ArgumentNullException("sprocParam");

            foreach (DbParameter p in _cmd.Parameters)
            {
                if (p.Direction != ParameterDirection.Input
                    && string.Compare(
                       p.ParameterName,
                       _builder.ToParameterName(sprocParam.ID),
                       StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (p.Value == DBNull.Value)
                        return null;
                    return (byte)p.Value;
                }
            }

            return null;
        }

        public short? GetOutputParameterValue(Int16ParameterExpression sprocParam)
        {
            if (ReferenceEquals(sprocParam, null))
                throw new ArgumentNullException("sprocParam");

            foreach (DbParameter p in _cmd.Parameters)
            {
                if (p.Direction != ParameterDirection.Input
                    && string.Compare(
                       p.ParameterName,
                       _builder.ToParameterName(sprocParam.ID),
                       StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (p.Value == DBNull.Value)
                        return null;
                    return (short)p.Value;
                }
            }

            return null;
        }

        public int? GetOutputParameterValue(Int32ParameterExpression sprocParam)
        {
            if (ReferenceEquals(sprocParam, null))
                throw new ArgumentNullException("sprocParam");

            foreach (DbParameter p in _cmd.Parameters)
            {
                if (p.Direction != ParameterDirection.Input
                    && string.Compare(
                       p.ParameterName,
                       _builder.ToParameterName(sprocParam.ID),
                       StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (p.Value == DBNull.Value)
                        return null;
                    return (int)p.Value;
                }
            }

            return null;
        }

        public long? GetOutputParameterValue(Int64ParameterExpression sprocParam)
        {
            if (ReferenceEquals(sprocParam, null))
                throw new ArgumentNullException("sprocParam");

            foreach (DbParameter p in _cmd.Parameters)
            {
                if (p.Direction != ParameterDirection.Input
                    && string.Compare(
                       p.ParameterName,
                       _builder.ToParameterName(sprocParam.ID),
                       StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (p.Value == DBNull.Value)
                        return null;
                    return (long)p.Value;
                }
            }

            return null;
        }

        public DateTime? GetOutputParameterValue(DateTimeParameterExpression sprocParam)
        {
            if (ReferenceEquals(sprocParam, null))
                throw new ArgumentNullException("sprocParam");

            foreach (DbParameter p in _cmd.Parameters)
            {
                if (p.Direction != ParameterDirection.Input
                    && string.Compare(
                       p.ParameterName,
                       _builder.ToParameterName(sprocParam.ID),
                       StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (p.Value == DBNull.Value)
                        return null;
                    return (DateTime)p.Value;
                }
            }

            return null;
        }

        public string GetOutputParameterValue(StringParameterExpression sprocParam)
        {
            if (ReferenceEquals(sprocParam, null))
                throw new ArgumentNullException("sprocParam");

            foreach (DbParameter p in _cmd.Parameters)
            {
                if (p.Direction != ParameterDirection.Input
                    && string.Compare(
                       p.ParameterName,
                       _builder.ToParameterName(sprocParam.ID),
                       StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (p.Value == DBNull.Value)
                        return null;
                    return (string)p.Value;
                }
            }

            return null;
        }

        public Guid? GetOutputParameterValue(GuidParameterExpression sprocParam)
        {
            if (ReferenceEquals(sprocParam, null))
                throw new ArgumentNullException("sprocParam");

            foreach (DbParameter p in _cmd.Parameters)
            {
                if (p.Direction != ParameterDirection.Input
                    && string.Compare(
                       p.ParameterName,
                       _builder.ToParameterName(sprocParam.ID),
                       StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (p.Value == DBNull.Value)
                        return null;
                    return (Guid)p.Value;
                }
            }

            return null;
        }

        public double? GetOutputParameterValue(DoubleParameterExpression sprocParam)
        {
            if (ReferenceEquals(sprocParam, null))
                throw new ArgumentNullException("sprocParam");

            foreach (DbParameter p in _cmd.Parameters)
            {
                if (p.Direction != ParameterDirection.Input
                    && string.Compare(
                       p.ParameterName,
                       _builder.ToParameterName(sprocParam.ID),
                       StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (p.Value == DBNull.Value)
                        return null;
                    return (double)p.Value;
                }
            }

            return null;
        }

        public decimal? GetOutputParameterValue(DecimalParameterExpression sprocParam)
        {
            if (ReferenceEquals(sprocParam, null))
                throw new ArgumentNullException("sprocParam");

            foreach (DbParameter p in _cmd.Parameters)
            {
                if (p.Direction != ParameterDirection.Input
                    && string.Compare(
                       p.ParameterName,
                       _builder.ToParameterName(sprocParam.ID),
                       StringComparison.OrdinalIgnoreCase) == 0)
                {
                    if (p.Value == DBNull.Value)
                        return null;
                    return (decimal)p.Value;
                }
            }

            return null;
        }

        #endregion
    }
}
