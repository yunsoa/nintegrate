using System;
using System.ComponentModel;
using System.IO;
using System.Web.UI;
using System.Drawing.Design;
using System.Web.UI.WebControls;
using System.Web;
using System.Security.Permissions;
using NIntegrate.Query;
using NIntegrate.Web.EventArgs;

namespace NIntegrate.Web
{
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal), 
        ParseChildren(true), PersistChildren(false)]
    public sealed class QueryDataSource : DataSourceControl
    {
        #region Private Fields

        private const string _defaultQueryServiceImplType = "NIntegrate.Query.Command.QueryService, NIntegrate.Query.Command";
        private QueryDataSourceView _view;
        private Criteria _criteria;
        private ParameterCollection _criteriaParameters;
        private IServiceLocator _locator;
        internal IQueryService _service;

        #endregion

        #region Protected Methods

        protected override DataSourceView GetView(string viewName)
        {
            if (_view == null)
                _view = new QueryDataSourceView(this);
            return _view;
        }

        protected override void OnInit(System.EventArgs e)
        {
            base.OnInit(e);

            if (HttpContext.Current == null)
                return;
            
            if (!UseLocalQueryService)
            {
                _locator = ServiceManager.GetServiceLocator(typeof(IQueryService));
                _service = _locator.GetService<IQueryService>();
            }
            else
            {
                var assemblyVersion = GetType().AssemblyQualifiedName.Substring(GetType().AssemblyQualifiedName.IndexOf(", Version="));
                var serviceType = Type.GetType(_defaultQueryServiceImplType + assemblyVersion);
                _service = (IQueryService)Activator.CreateInstance(serviceType);
                if (_service == null)
                    throw new FileLoadException("Could not load assembly - NIntegrate.Query.Command.dll.");
            }
        }

        #endregion

        #region Public Properties

        [Category("Data"), DefaultValue(false), Description("When the value of this property equals true, it always using NIntegrate.Query.Command.QueryService class as QueryService insteads of trying to get the IQueryService implementation instance from ServiceManager class.")]
        public bool UseLocalQueryService { get; set; }

        [Category("Data"), Description("Specify the criteria.")]
        public Criteria Criteria
        {
            internal get
            {
                if (_criteria == null && EnableViewState 
                    && ViewState["Criteria"] != null)
                {
                    _criteria = QueryHelper.CriteriaDeserialize(
                        (string)ViewState["Criteria"]);
                }

                return _criteria;
            }
            set
            {
                if (value == null)
                    return;

                _criteria = value;
                if (EnableViewState)
                    ViewState["Criteria"] = QueryHelper.CriteriaSerialize(value.ToBaseCriteria());
            }
        }

        [Category("Data"), DefaultValue(ConflictOptions.OverwriteChanges), Description("Conflict detection when updating or deleting data.")]
        public ConflictOptions ConflictDetection { get; set; }

        [Category("Data"), DefaultValue(false), Description("Always append default SortBys specified Criteria as secondary SortBys when sorting.")]
        public bool AlwaysAppendDefaultSortBysWhenSorting { get; set; }

        [Editor("System.Web.UI.Design.WebControls.ParameterCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), DefaultValue((string)null), MergableProperty(false), PersistenceMode(PersistenceMode.InnerProperty), Category("Data"), Description("CriteriaParameters")]
        public ParameterCollection CriteriaParameters
        {
            get
            {
                if (_criteriaParameters == null)
                {
                    _criteriaParameters = new ParameterCollection();
                    _criteriaParameters.ParametersChanged += ((QueryDataSourceView)GetView(null)).SelectParametersChangedEventHandler;
                }
                return _criteriaParameters;
            }
        }

        public int LastTotalCount
        {
            get
            {
                return ViewState["LastTotalCount"] == null ? 0 : (int)ViewState["LastTotalCount"];
            }
            internal set { ViewState["LastTotalCount"] = value; }
        }

        #endregion

        #region Events

        /// <summary>
        /// This event occurs after an object is deleted. Handle this event to update your data store.
        /// </summary>
        [Category("Data"),
        Description("Occurs after an object is deleted. Handle this event to update your data store.")]
        public event EventHandler<DataSourceStatusEventArgs> Deleted;

        internal void OnDeleted(DataSourceStatusEventArgs args)
        {
            if (Deleted != null)
                Deleted(this, args);
        }

        /// <summary>
        /// This event occurs after an object is deleted. Handle this event to update your data store.
        /// </summary>
        [Category("Data"),
       Description("Occurs after an object is deleted. Handle this event to update your data store.")]
        public event EventHandler<DataSourceStatusEventArgs> Updated;

        internal void OnUpdated(DataSourceStatusEventArgs args)
        {
            if (Updated != null)
                Updated(this, args);
        }

        /// <summary>
        /// This event occurs after an object is inserted.
        /// </summary>
        [Category("Data"),
        Description("Occurs after an object is inserted. Handle this event to update your data store.")]
        public event EventHandler<DataSourceStatusEventArgs> Inserted;

        internal void OnInserted(DataSourceStatusEventArgs args)
        {
            if (Inserted != null)
                Inserted(this, args);
        }

        /// <summary>
        /// This event occurs before an object is deleted. Handle this event to validate and/or modify the input parameters.
        /// </summary>
        [Category("Data"),
        Description("Occurs before an object is deleted. Handle this event to validate and/or modify the input parameters.")]
        public event EventHandler<DataSourceDeletingEventArgs> Deleting;

        internal void OnDeleting(DataSourceDeletingEventArgs args)
        {
            if (Deleting != null)
                Deleting(this, args);
        }

        /// <summary>
        /// This event occurs before an object is updated. Handle this event to validate and/or modify the input parameters.
        /// </summary>
        [Category("Data"),
        Description("Occurs before an object is updated. Handle this event to validate and/or modify the input parameters.")]
        public event EventHandler<DataSourceUpdatingEventArgs> Updating;

        internal void OnUpdating(DataSourceUpdatingEventArgs args)
        {
            if (Updating != null)
                Updating(this, args);
        }

        /// <summary>
        /// This event occurs before an object is inserted. Handle this event to validate and/or modify the input parameters.
        /// </summary>
        [Category("Data"),
        Description("Occurs before an object is inserted. Handle this event to validate and/or modify the input parameters.")]
        public event EventHandler<DataSourceInsertingEventArgs> Inserting;

        internal void OnInserting(DataSourceInsertingEventArgs args)
        {
            if (Inserting != null)
                Inserting(this, args);
        }

        /// <summary>
        /// This event occurs before performing a select operation. Handle this event to override the default select behavior.
        /// </summary>
        [Category("Data"),
        Description("Occurs before performing a select operation.")]
        public event EventHandler<DataSourceSelectingEventArgs> Selecting;

        internal void OnSelecting(DataSourceSelectingEventArgs args)
        {
            if (Selecting != null)
                Selecting(this, args);
        }

        #endregion

        #region Dispose()

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed;

        private void Dispose(bool disposing)
        {
            if (disposed) return;
            if (disposing)
            {
                var dispose = _service as IDisposable;
                if (dispose != null)
                    dispose.Dispose();
                if (_locator != null)
                    _locator.Dispose();
            }

            disposed = true;
        }

        ~QueryDataSource()
        {
            Dispose(false);
        }

        #endregion
    }
}
