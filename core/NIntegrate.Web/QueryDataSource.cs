using System;
using System.ComponentModel;
using System.ServiceModel;
using System.Web.UI;
using System.Drawing.Design;
using System.Web.UI.WebControls;
using System.Web;
using System.Security.Permissions;
using NIntegrate.Data;
using NIntegrate.ServiceModel;
using NIntegrate.ServiceModel.Activation;
using NIntegrate.ServiceModel.Configuration;
using NIntegrate.Web.EventArgs;

namespace NIntegrate.Web
{
    /// <summary>
    /// The DataSourceControl implementation based on NIntegrate.Query.IQueryService.
    /// </summary>
    [AspNetHostingPermission(SecurityAction.Demand, Level = AspNetHostingPermissionLevel.Minimal)]
    [ParseChildren(true)]
    [PersistChildren(false)]
    public sealed class QueryDataSource : DataSourceControl
    {
        private QueryDataSourceView _view;
        private QueryCriteria _criteria;
        private ParameterCollection _criteriaParameters;
        private WcfClientEndpoint _endpoint;
        private bool _disposed;
        private IQueryService _service;

        #region Properties

        /// <summary>
        /// Specify a WCF endpoint configuration for query service.
        /// If not specified, local query service is used.
        /// </summary>
        public WcfClientEndpoint Endpoint
        {
            internal get
            {
                if (_endpoint == null && EnableViewState
                    && ViewState["Endpoint"] != null)
                {
                    _endpoint = ExpressionHelper.Deserialize<WcfClientEndpoint>(
                        (string)ViewState["Endpoint"]);
                }

                return _endpoint;
            }
            set
            {
                if (value == null)
                    return;

                _endpoint = value;
                if (EnableViewState)
                    ViewState["Endpoint"] = ExpressionHelper.Serialize(value);
            }
        }

        internal IQueryService QueryService
        {
            get
            {
                if (_service == null)
                {
                    var endpoint = Endpoint;
                    if (endpoint != null)
                    {
                        _service = WcfChannelFactoryFactory.CreateChannelFactory<IQueryService>(endpoint).CreateChannel();
                    }
                    else
                    {
                        _service = new QueryService();
                    }
                }

                return _service;
            }
        }

        /// <summary>
        /// Specify the assembly qualified type name of query table.
        /// </summary>
        [Category("Data"), DefaultValue(""), Description("Specify the type full name or qualified name of query table.")]
        public string QueryTableType
        {
            set
            {
                if (Criteria == null && !string.IsNullOrEmpty(value))
                {
                    var type = WcfServiceHostFactory.GetType(value, true);
                    if (type != null)
                    {
                        var instance = Activator.CreateInstance(type) as QueryTable;
                        if (instance != null)
                        {
                            Criteria = instance.Select();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the query criteria.
        /// </summary>
        /// <value>The criteria.</value>
        public QueryCriteria Criteria
        {
            get
            {
                if (_criteria == null && EnableViewState 
                    && ViewState["Criteria"] != null)
                {
                    _criteria = ExpressionHelper.Deserialize<QueryCriteria>(
                        (string)ViewState["Criteria"]);
                }

                return _criteria;
            }
            set
            {
                if (value == null)
                    return;

                _criteria = value.Clone();
                if (EnableViewState)
                {
                    _criteria.Changed +=
                        ((sender, args) => ViewState["Criteria"] = ExpressionHelper.Serialize(sender as QueryCriteria));

                    ViewState["Criteria"] = ExpressionHelper.Serialize(value);
                }
            }
        }

        /// <summary>
        /// Gets or sets the conflict detection.
        /// </summary>
        /// <value>The conflict detection.</value>
        [Category("Data"), DefaultValue(ConflictOptions.OverwriteChanges), Description("Conflict detection when updating or deleting data.")]
        public ConflictOptions ConflictDetection { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether always append default sort bys when sorting.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if always append default sort bys when sorting; otherwise, <c>false</c>.
        /// </value>
        [Category("Data"), DefaultValue(false), Description("Always append default SortBys specified Criteria as secondary SortBys when sorting.")]
        public bool AlwaysAppendDefaultSortBysWhenSorting { get; set; }

        /// <summary>
        /// Gets the query criteria parameters.
        /// </summary>
        /// <value>The query criteria parameters.</value>
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
        /// Gets the last total count.
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

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            if (disposing)
            {
                //close channel factory in best practice
                //refer to: http://bloggingabout.net/blogs/erwyn/archive/2006/12/09/WCF-Service-Proxy-Helper.aspx
                ICommunicationObject commObj = null;
                try
                {
                    commObj = _service as ICommunicationObject;
                    if (commObj != null)
                        commObj.Close();

                    var dispose = _service as IDisposable;
                    if (dispose != null)
                        dispose.Dispose();
                }
                catch (CommunicationException)
                {
                    if (commObj != null)
                        commObj.Abort();
                }
                catch (TimeoutException)
                {
                    if (commObj != null)
                        commObj.Abort();
                }
                catch (Exception)
                {
                    if (commObj != null)
                        commObj.Abort();

                    throw;
                }
            }

            _disposed = true;
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

        #region Non-Public Methods

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
        }

        #endregion
    }
}
