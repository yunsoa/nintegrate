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
using EAppointments.UI.Modules;
using EAppointments.UI.Modules.Views;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb.Interfaces;
using Microsoft.Practices.CompositeWeb;
using EAppointments.UI.ServiceAgents.ProviderService;

public partial class Provider_SlotList : UserControl, ISlotListView
{
    private SlotListViewPresenter _presenter;
    private DateTime _startDate;
    private DateTime _endDate;
    private Int32 _weekDays;
    private Guid _providerId;
    private Guid _clinicTypeId;
   
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
        SlotDataSource.DataBind();
    }

    [CreateNew]
    public SlotListViewPresenter Presenter
    {
        get { return _presenter; }
        set
        {
            _presenter = value;
            _presenter.View = this;
        }
    }

    public DateTime StartDate { get { return _startDate; } set { _startDate = value; } }
    public DateTime EndDate { get { return _endDate; } set { _endDate = value; } }
    public Int32 WeekDays { get { return _weekDays; } set { _weekDays = value; } }
    public Guid ProviderId { get { return _providerId; } set { _providerId = value; } }
    public Guid ClinicTypeId { get { return _clinicTypeId; } set { _clinicTypeId = value; } }

    public Slot[] Slots
    {
        set { SlotDataSource.DataSource = value; }        
    }

    public Guid? SelectedSlotId
    {
        get
        {
            if (SlotGrid.SelectedIndex >= 0)
                return (Guid)SlotGrid.SelectedDataKey.Value;
            else
                return null;
        }
    }

    public void BindData()
    {
        _presenter.OnViewLoaded();
        SlotGrid.DataBind();
    }
}