using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Collections;
using NIntegrate.Mapping;
using System.Data;

namespace NIntegrate.Data
{
    public interface IActiveRecordConnection
    {
        int ExecuteNonQuery(QueryCriteria criteria);
        object ExecuteScalar(QueryCriteria criteria);
    }

    public interface IActiveRecordConnection<TRecord> : IActiveRecordConnection
        where TRecord : ActiveRecord
    {
        TRecord ExecuteOne(QueryCriteria criteria);
        ICollection<TRecord> ExecuteMany(QueryCriteria criteria);
        int ExecuteCount(QueryCriteria criteria);
    }

    public class LocalActiveRecordConnection<TRecord> : IActiveRecordConnection<TRecord>
        where TRecord : ActiveRecord
    {
        private readonly QueryCommandFactory _cmdFactory;
        private static readonly MapperFactory _mapperFactory;
        private static readonly Mapper<IDataReader, TRecord> _mapperOne;
        private static readonly Mapper<IDataReader, List<TRecord>> _mapperMany;

        #region Constructors

        static LocalActiveRecordConnection()
        {
            _mapperFactory = new MapperFactory();

            _mapperFactory.ConfigureMapper<IDataReader, TRecord>(true, true, true, "Q");
            _mapperOne = _mapperFactory.GetMapper<IDataReader, TRecord>();

            _mapperFactory.ConfigureMapper<IDataReader, List<TRecord>>(true, true, true);
            _mapperMany = _mapperFactory.GetMapper<IDataReader, List<TRecord>>();
        }

        public LocalActiveRecordConnection(QueryCommandFactory cmdFactory)
        {
            if (cmdFactory == null)
                throw new ArgumentNullException("cmdFactory");

            _cmdFactory = cmdFactory;
        }

        #endregion

        public int ExecuteNonQuery(QueryCriteria criteria)
        {
            using (var cmd = _cmdFactory.CreateCommand(criteria, false))
            {
                using (var conn = cmd.Connection)
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public object ExecuteScalar(QueryCriteria criteria)
        {
            using (var cmd = _cmdFactory.CreateCommand(criteria, false, true))
            {
                using (var conn = cmd.Connection)
                {
                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        public TRecord ExecuteOne(QueryCriteria criteria)
        {
            using (var cmd = _cmdFactory.CreateCommand(criteria, false))
            {
                using (var conn = cmd.Connection)
                {
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            var one = _mapperOne(rdr);
                            one.Attach(this);
                            return one;
                        }
                    }
                }
            }

            return default(TRecord);
        }

        public ICollection<TRecord> ExecuteMany(QueryCriteria criteria)
        {
            using (var cmd = _cmdFactory.CreateCommand(criteria, false))
            {
                using (var conn = cmd.Connection)
                {
                    conn.Open();
                    using (var rdr = cmd.ExecuteReader())
                    {
                        var many = _mapperMany(rdr);
                        if (many != null)
                        {
                            foreach (var one in many)
                            {
                                one.Attach(this);
                            }
                        }
                        return many;
                    }
                }
            }
        }

        public int ExecuteCount(QueryCriteria criteria)
        {
            using (var cmd = _cmdFactory.CreateCommand(criteria, true))
            {
                using (var conn = cmd.Connection)
                {
                    conn.Open();

                    return Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
        }
    }
}
