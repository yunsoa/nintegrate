//===============================================================================
// Microsoft Aspiring Software Architects Program
// E-Appointments - Case Study Implementation
//===============================================================================
// Copyright  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===============================================================================

using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using EAppointments.UI.ServiceAgents.AppointmentService;
using EAppointments.UI.Modules;
using EAppointments.UI.Modules.Views;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb.Interfaces;
using Microsoft.Practices.CompositeWeb;


public partial class Appointment_List : UserControl, IAppointmentListView
{
    private AppointmentListViewPresenter _presenter;
    private DateTime? _startDateTime;
    private DateTime? _endDateTime;
    private int _status;
    private Guid? _patientId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            _presenter.OnViewInitialized();
        }
        BindData();
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        AppointmentDataSource.DataBind();   
    }

    [CreateNew]
    public AppointmentListViewPresenter Presenter
    {
        get { return _presenter; }
        set
        {
            _presenter = value;
            _presenter.View = this;
        }
    }

    public DateTime? StartDateTime
    {
        get { return _startDateTime; }
        set { _startDateTime = value; }
    }

    public DateTime? EndDateTime
    {
        get { return _endDateTime; }
        set { _endDateTime = value; }
    }

    public int Status
    {
        get { return _status; }
        set { _status = value; }
    }

    public Guid? PatientId
    {
        get { return _patientId; }
        set { _patientId = value; }
    }

    public Appointment[] Appointments
    {
        set { AppointmentDataSource.DataSource = value; }
    }

    public void BindData()
    {
        _presenter.OnViewLoaded();
        AppointmentGrid.DataBind(); 
    }

    public int? SelectedUbrn
    {
        get
        {
            if (AppointmentGrid.SelectedIndex >= 0)
                return (int?)AppointmentGrid.SelectedDataKey.Value;
            else
                return null;
        }
    }

    private static readonly object EventRowSelected = new object();
    public event EventHandler<RowSelectedEventArgs> RowSelected
    {
        add
        {
            base.Events.AddHandler(EventRowSelected, value);
        }
        remove
        {
            base.Events.RemoveHandler(EventRowSelected, value);
        }
    }

    protected virtual void OnRowSelected(RowSelectedEventArgs e)
    {
        EventHandler<RowSelectedEventArgs> handler = (EventHandler<
          RowSelectedEventArgs>)base.Events[EventRowSelected];
        if (null != handler)
            handler(this, e);
    }
    
    protected void AppointmentGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        int ubrn = (int)AppointmentGrid.SelectedDataKey.Value;

        RowSelectedEventArgs args = new RowSelectedEventArgs(ubrn);
        
        // Fire the event handler giving the host page chance to handle 
        // this event in its context.
        OnRowSelected(args);
    }
}
