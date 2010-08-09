﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using NIntegrate.Data.Configuration;
using System.Reflection;
using NIntegrate.Mapping;

namespace NIntegrate.Data
{
    /// <summary>
    /// An ObjectId is an identity of an ActiveRecord
    /// </summary>
    /// <typeparam name="TRecord">The type of the ActiveRecord</typeparam>
    /// <typeparam name="TQuery">The type of the query class for the ActiveRecord</typeparam>
    [DataContract(Namespace = "http://nintegrate.com")]
    [KnownType("KnownTypes")]
    public sealed class ObjectId<TRecord, TQuery>
        where TRecord : ActiveRecord
        where TQuery : QueryTable, new()
    {
        [DataMember]
        private readonly Condition _condition;
        [DataMember]
        private readonly bool _autoGenerated;

        #region Properties

        /// <summary>
        /// If the ObjectId value is auto generated by database on creating
        /// </summary>
        public bool AutoGenerated
        {
            get { return _autoGenerated; }
        }

        #endregion

        #region KnownTypes

        /// <summary>
        /// The known types for serialization
        /// </summary>
        /// <returns>The known types</returns>
        static Type[] KnownTypes()
        {
            return KnownTypeRegistry.Instance.KnownTypes;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Create an instance of ObjectId
        /// </summary>
        /// <param name="condition">The query condition</param>
        /// <param name="autoGenerated">Whether the ObjectId is auto generated by database on creating</param>
        public ObjectId(Condition condition, bool autoGenerated)
        {
            if (Equals(condition, null))
                throw new ArgumentNullException("condition");

            _condition = condition;
            _autoGenerated = autoGenerated;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Convert the ObjectId instance to a query criteria
        /// </summary>
        /// <returns></returns>
        public QueryCriteria ToCriteria()
        {
            var query = ActiveRecord<TRecord, TQuery>.Q;
            return new QueryCriteria(query.TableName, query.ConnectionStringName, query.ReadOnly, query.PredefinedColumns)
                .Where(_condition);
        }

        #endregion
    }

    /// <summary>
    /// The root base class of any ActiveRecord
    /// </summary>
    [DataContract(Namespace = "http://nintegrate.com")]
    public abstract class ActiveRecord
    {
        [DataMember]
        private bool _isNew = true;

        private IActiveRecordConnection _connection;

        #region Constructors

        internal ActiveRecord() { }

        #endregion

        #region Public Methods

        /// <summary>
        /// Whether the ActiveRecord instance is saved or new
        /// </summary>
        /// <returns>Is new or not</returns>
        public bool IsNew()
        {
            return _isNew;
        }

        /// <summary>
        /// Attach an ActiveRecord instance to a connection,
        /// the attach could not be serialized, which means after serialize and deserialize, 
        /// an ActiveRecord instance's IsAttached() becomes false.
        /// </summary>
        /// <param name="cmdFactory"></param>
        public void Attach(IActiveRecordConnection connection)
        {
            _connection = connection;
        }

        /// <summary>
        /// Whether the ActiveRecord instance is attached to a connection
        /// </summary>
        /// <returns>Is attached or not</returns>
        public bool IsAttached()
        {
            return _connection != null;
        }

        /// <summary>
        /// Insert/Update the instance to database
        /// </summary>
        /// <param name="assignments">
        ///     The specified columns to save, if not specified, save all the assignments specified in
        ///     GetSaveAssignments() or GetInsertAssignments()
        /// </param>
        /// <returns>Whether saving is successful or not</returns>
        public abstract bool Save(params Assignment[] assignments);

        /// <summary>
        /// Execute a custom update criteria created through ActiveRecord.Q.Update()
        /// </summary>
        /// <param name="criteria">The update criteria to execute</param>
        /// <returns>How many rows updated in database</returns>
        public abstract int Save(QueryCriteria criteria);

        /// <summary>
        /// Delete the current instance
        /// </summary>
        /// <returns>Whether deleting is successful or not</returns>
        public abstract bool Delete();

        /// <summary>
        /// Execute a custom delete criteria created through ActiveRecord.Q.Delete()
        /// </summary>
        /// <param name="criteria">The delete criteria</param>
        /// <returns></returns>
        public abstract int Delete(QueryCriteria criteria);

        /// <summary>
        /// Execute a custom count criteria created through ActiveRecord.Q.Select()
        /// </summary>
        /// <param name="criteria">The count criteria</param>
        /// <returns></returns>
        public abstract int Count(QueryCriteria criteria);

        #endregion

        #region Non-Public Methods

        protected void SetIsNew(bool isNew)
        {
            _isNew = isNew;
        }

        protected IActiveRecordConnection<TRecord> GetConnection<TRecord>()
            where TRecord : ActiveRecord
        {
            var connection = _connection as IActiveRecordConnection<TRecord>;
            if (connection == null)
                throw new InvalidCastException(string.Format("Failed to cast connection to IActiveRecordConnection<{0}>", typeof(TRecord).FullName));

            return connection;
        }

        #endregion
    }

    /// <summary>
    /// The abstract base class of all ActiveRecord
    /// </summary>
    /// <typeparam name="TRecord">The type of the ActiveRecord</typeparam>
    /// <typeparam name="TQuery">The type of the query class for the ActiveRecord</typeparam>
    [DataContract(Namespace = "http://nintegrate.com")]
    public abstract class ActiveRecord<TRecord, TQuery> : ActiveRecord
        where TRecord : ActiveRecord
        where TQuery : QueryTable, new()
    {
        public static readonly TQuery Q = new TQuery();

        #region Public Methods

        /// <summary>
        /// Determines whether the ActiveRecord is ReadOnly.
        /// </summary>
        /// <returns>
        /// 	<c>true</c> if the ActiveRecord is ReadOnly; otherwise, <c>false</c>.
        /// </returns>
        public bool IsReadOnly()
        {
            return Q.ReadOnly;
        }

        /// <summary>
        /// Get the ObjectId of the current instance.
        /// </summary>
        /// <returns>The ObjectId</returns>
        public abstract ObjectId<TRecord, TQuery> GetObjectId();

        /// <summary>
        /// Get the save assignments.
        /// </summary>
        /// <returns>The save assignments</returns>
        public abstract Assignment[] GetSaveAssignments();

        /// <summary>
        /// Get the insert assignments.
        /// </summary>
        /// <returns>The insert assignments</returns>
        public virtual Assignment[] GetInsertAssignments()
        {
            return GetSaveAssignments();
        }

        /// <summary>
        /// Set the auto generated id value,
        /// Only to be implemented in child class for saveable ID AutoGenerated ActiveRecord
        /// </summary>
        /// <param name="value">The value.</param>
        public virtual void SetAutoGeneratedIdValue(object value)
        {
        }

        /// <summary>
        /// Execute a custom update criteria created through ActiveRecord.Q.Update()
        /// </summary>
        /// <param name="criteria">The update criteria to execute</param>
        /// <returns>How many rows updated in database</returns>
        public sealed override int Save(QueryCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            if (criteria.QueryType != QueryType.Update || criteria.TableName != Q.TableName)
                throw new ArgumentException("criteria invalid");

            if (!IsAttached())
                throw new InvalidOperationException("not attached");

            if (IsReadOnly())
                throw new InvalidOperationException("readonly");

            return GetConnection<TRecord>().ExecuteNonQuery(criteria);
        }

        /// <summary>
        /// Insert/Update the instance to database
        /// </summary>
        /// <param name="assignments">The specified columns to save, if not specified, save all the assignments specified in
        /// GetSaveAssignments() or GetInsertAssignments()</param>
        /// <returns>Whether saving is successful or not</returns>
        public sealed override bool Save(params Assignment[] assignments)
        {
            if (!IsAttached())
                throw new InvalidOperationException("not attached");

            if (IsReadOnly())
                throw new InvalidOperationException("readonly");

            var objectId = GetObjectId();

            if (objectId == null)
                throw new InvalidOperationException("GetObjectId() is null");

            var criteria = objectId.ToCriteria();
            criteria.QueryType = (IsNew() ? QueryType.Insert : QueryType.Update);

            ResolveaSaveAssignments(criteria, assignments);

            var result = false;

            if (criteria.QueryType == QueryType.Insert && objectId.AutoGenerated)
            {
                var autoGeneratedId = GetConnection<TRecord>().ExecuteScalar(criteria);
                result = (autoGeneratedId != DBNull.Value && autoGeneratedId != null);

                if (result)
                    SetAutoGeneratedIdValue(autoGeneratedId);
            }
            else
            {
                result = GetConnection<TRecord>().ExecuteNonQuery(criteria) > 0;
            }

            if (result)
                SetIsNew(false);

            return result;
        }

        /// <summary>
        /// Execute a custom delete criteria created through ActiveRecord.Q.Delete()
        /// </summary>
        /// <param name="criteria">The delete criteria</param>
        /// <returns></returns>
        public sealed override int Delete(QueryCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            if (criteria.QueryType != QueryType.Delete || criteria.TableName != Q.TableName)
                throw new ArgumentException("criteria invalid");

            if (!IsAttached())
                throw new InvalidOperationException("not attached");

            if (IsReadOnly())
                throw new InvalidOperationException("readonly");

            return GetConnection<TRecord>().ExecuteNonQuery(criteria);
        }

        /// <summary>
        /// Delete the current instance
        /// </summary>
        /// <returns>Whether deleting is successful or not</returns>
        public sealed override bool Delete()
        {
            if (!IsAttached())
                throw new InvalidOperationException("not attached");

            if (IsReadOnly())
                throw new InvalidOperationException("readonly");

            bool result = false;

            var objectId = GetObjectId();
            if (objectId == null)
                throw new InvalidOperationException("GetObjectId() is null");

            var criteria = objectId.ToCriteria();
            criteria.QueryType = QueryType.Delete;

            result = GetConnection<TRecord>().ExecuteNonQuery(criteria) > 0;

            if (result)
                SetIsNew(true);

            return result;
        }

        /// <summary>
        /// Find one ActiveRecord instance by specified ObjectId.
        /// </summary>
        /// <param name="id">The ObjectId.</param>
        /// <returns>The matching ActiveRecord, return null if not found</returns>
        public TRecord FindOne(ObjectId<TRecord, TQuery> id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            var criteria = id.ToCriteria();
            criteria.QueryType = QueryType.Select;
            return FindOne(criteria);
        }

        /// <summary>
        /// Find one ActiveRecord instance by specified query criteria.
        /// </summary>
        /// <param name="criteria">The select or sproc criteria.</param>
        /// <returns>The matching ActiveRecord, return null if not found</returns>
        public TRecord FindOne(QueryCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            if (criteria.TableName != Q.TableName
                || (criteria.QueryType != QueryType.Select && criteria.QueryType != QueryType.Sproc))
            {
                throw new ArgumentException("criteria invalid");
            }

            if (!IsAttached())
                throw new InvalidOperationException("not attached");

            var one = GetConnection<TRecord>().ExecuteOne(criteria);
            one.Attach(GetConnection<TRecord>());
            return one;
        }

        /// <summary>
        /// Find many ActiveRecord instance by specified query criteria.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The matching ActiveRecord collection, return null if not found</returns>
        public ICollection<TRecord> FindMany(QueryCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            if (criteria.TableName != Q.TableName
                || (criteria.QueryType != QueryType.Select && criteria.QueryType != QueryType.Sproc))
            {
                throw new ArgumentException("criteria invalid");
            }

            if (!IsAttached())
                throw new InvalidOperationException("not attached");

            var many = GetConnection<TRecord>().ExecuteMany(criteria);
            if (many != null && many.Count > 0)
            {
                foreach (var one in many)
                {
                    one.Attach(GetConnection<TRecord>());
                }
            }
            return many;
        }

        /// <summary>
        /// Execute a custom count criteria created through ActiveRecord.Q.Select()
        /// </summary>
        /// <param name="criteria">The count criteria</param>
        /// <returns></returns>
        public override int Count(QueryCriteria criteria)
        {
            if (criteria == null)
                throw new ArgumentNullException("criteria");

            if (criteria.TableName != Q.TableName
                || (criteria.QueryType != QueryType.Select && criteria.QueryType != QueryType.Sproc))
            {
                throw new ArgumentException("criteria invalid");
            }

            if (!IsAttached())
                throw new InvalidOperationException("not attached");

            return GetConnection<TRecord>().ExecuteCount(criteria);
        }

        #endregion

        #region Non-Public Methods

        private void ResolveaSaveAssignments(QueryCriteria criteria, Assignment[] assignments)
        {
            if (assignments != null && assignments.Length > 0)
                criteria.SetAssignments(assignments);
            else if (criteria.QueryType == QueryType.Insert)
                criteria.SetAssignments(GetInsertAssignments());
            else
                criteria.SetAssignments(GetSaveAssignments());
        }

        #endregion
    }
}
