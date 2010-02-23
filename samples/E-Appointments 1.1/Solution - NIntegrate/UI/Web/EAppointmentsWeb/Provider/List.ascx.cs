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
using System.Web.Services;
using System.Web.Script.Services;

public partial class Provider_List : UserControl, IProviderListView
{
    private ProviderListViewPresenter _presenter;
    private Guid _clinicTypeId;
    private String _keywords;
    private Double? _withinMiles;
    private String _zipCode;

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
        ProviderDataSource.DataBind();
    }

    [CreateNew]
    public ProviderListViewPresenter Presenter
    {
        get { return _presenter; }
        set
        {
            _presenter = value;
            _presenter.View = this;
        }
    }

    public String Keywords { get { return _keywords; } set { _keywords = value; } }
    public Guid ClinicTypeId { get { return _clinicTypeId; } set { _clinicTypeId = value; } }
    public Double? WithinMiles { get { return _withinMiles; } set { _withinMiles = value; } }
    public String ZipCode { get { return _zipCode; } set { _zipCode = value; } }

    public Provider[] Providers
    {
        set { ProviderDataSource.DataSource = value; }
    }

    public Guid? SelectedProviderId
    {
        get
        {
            if (ProviderGrid.SelectedIndex >= 0)
                return (Guid)ProviderGrid.SelectedDataKey.Value;
            else
                return null;
        }
    }

    public void BindData()
    {
        _presenter.OnViewLoaded();
        ProviderGrid.DataBind();
    }
}