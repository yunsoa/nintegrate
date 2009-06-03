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
    /// <summary>
    /// The DataSourceControl implementation based on NIntegrate.Query.IQueryService.
    /// </summary>
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

        /// <summary>
        /// Gets the named data source view associated with the data source control.
        /// </summary>
        /// <param name="viewName">The name of the <see cref="T:System.Web.UI.DataSourceView"/> to retrieve. In data source controls that support only one view, such as <see cref="T:System.Web.UI.WebControls.SqlDataSource"/>, this parameter is ignored.</param>
        /// <returns>
        /// Returns the named <see cref="T:System.Web.UI.DataSourceView"/> associated with the <see cref="T:System.Web.UI.DataSourceControl"/>.
        /// </returns>
        protected override DataSourceView GetView(string viewName)
        {
            if (_view == null)
                _view = new QueryDataSourceView(this);
            return _view;
        }

        /// <summary>
        /// Raises the <see cref="E:System.Web.UI.Control.Init"/> event.
        /// </summary>
        /// <param name="e">An <see cref="T:System.EventArgs"/> object that contains the event data.</param>
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

        /// <summary>
        /// When the value of this property equals true, it always using NIntegrate.Query.Command.QueryService class as QueryService insteads of trying to get the IQueryService implementation instance from ServiceManager class.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if use local query service; otherwise, <c>false</c>.
        /// </value>
        [Category("Data"), DefaultValue(false), Description("When the value of this property equals true, it always using NIntegrate.Query.Command.QueryService class as QueryService insteads of trying to get the IQueryService implementation instance from ServiceManager class.")]
        public bool UseLocalQueryService { get; set; }

        /// <summary>
        /// Gets or sets the criteria.
        /// </summary>
        /// <value>The criteria.</value>
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
                    ViewState["Criteria"] = QueryHelper.CriteriaSerialize(value.ToSerializableCriteria());
            }
        }

        /// <summary>
        /// Gets or sets the conflict detection.
        /// </summary>
        /// <value>The conflict detection.</value>
        [Category("Data"), DefaultValue(ConflictOptions.OverwriteChanges), Description("Conflict detection when updating or deleting data.")]
        public ConflictOptions ConflictDetection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [always append default sort bys when sorting].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [always append default sort bys when sorting]; otherwise, <c>false</c>.
        /// </value>
        [Category("Data"), DefaultValue(false), Description("Always append default SortBys specified Criteria as secondary SortBys when sorting.")]
        public bool AlwaysAppendDefaultSortBysWhenSorting { get; set; }

        /// <summary>
        /// Gets the criteria parameters.
        /// </summary>
        /// <value>The criteria parameters.</value>
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

        /// <summary>
        /// Gets or sets the last total count.
        /// </summary>
        /// <value>The last total count.</value>
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

        /// <summary>
        /// Raises the <see cref="Deleted"/> event.
        /// </summary>
        /// <param name="args">The <see cref="NIntegrate.Web.EventArgs.DataSourceStatusEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Raises the <see cref="Updated"/> event.
        /// </summary>
        /// <param name="args">The <see cref="NIntegrate.Web.EventArgs.DataSourceStatusEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Raises the <see cref="Inserted"/> event.
        /// </summary>
        /// <param name="args">The <see cref="NIntegrate.Web.EventArgs.DataSourceStatusEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Raises the <see cref="Deleting"/> event.
        /// </summary>
        /// <param name="args">The <see cref="NIntegrate.Web.EventArgs.DataSourceDeletingEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Raises the <see cref="Updating"/> event.
        /// </summary>
        /// <param name="args">The <see cref="NIntegrate.Web.EventArgs.DataSourceUpdatingEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Raises the <see cref="Inserting"/> event.
        /// </summary>
        /// <param name="args">The <see cref="NIntegrate.Web.EventArgs.DataSourceInsertingEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Raises the <see cref="Selecting"/> event.
        /// </summary>
        /// <param name="args">The <see cref="NIntegrate.Web.EventArgs.DataSourceSelectingEventArgs"/> instance containing the event data.</param>
        internal void OnSelecting(DataSourceSelectingEventArgs args)
        {
            if (Selecting != null)
                Selecting(this, args);
        }

        /// <summary>
        /// This event occurs after performing a select operation. Handle this event to override the default select behavior.
        /// </summary>
        [Category("Data"),
        Description("Occurs after performing a select operation.")]
        public event EventHandler<DataSourceSelectedEventArgs> Selected;

        /// <summary>
        /// Raises the <see cref="Selected"/> event.
        /// </summary>
        /// <param name="args">The <see cref="NIntegrate.Web.EventArgs.DataSourceSelectedEventArgs"/> instance containing the event data.</param>
        internal void OnSelected(DataSourceSelectedEventArgs args)
        {
            if (Selected != null)
                Selected(this, args);
        }

        #endregion

        #region Dispose()

        /// <summary>
        /// Enables a server control to perform final clean up before it is released from memory.
        /// </summary>
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

        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="QueryDataSource"/> is reclaimed by garbage collection.
        /// </summary>
        ~QueryDataSource()
        {
            Dispose(false);
        }

        #endregion
    }
}
