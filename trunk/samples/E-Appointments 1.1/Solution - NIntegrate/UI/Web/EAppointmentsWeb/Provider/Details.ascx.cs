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
using EAppointments.UI.Modules.Views;
using Microsoft.Practices.ObjectBuilder;
using EAppointments.UI.ServiceAgents.ProviderService;

public partial class Provider_Details : UserControl, IProviderDetailsView
{
    private ProviderDetailsViewPresenter _presenter;
    private Guid? _providerId;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            _presenter.OnViewInitialized();
        }
        BindData(this._providerId);
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        ProviderDataSource.DataBind();
    }

    [CreateNew]
    public ProviderDetailsViewPresenter Presenter
    {
        get { return _presenter; }
        set
        {
            _presenter = value;
            _presenter.View = this;
        }
    }

    public Guid? ProviderId
    {
        get { return _providerId; }
        set { _providerId = value; }
    }

    public Provider CurrentProvider
    {
        set { ProviderDataSource.DataSource = value; }
    }

    public void BindData(Guid? providerId)
    {
        _providerId = providerId;
        if (_providerId.HasValue)
            _presenter.OnViewLoaded();
        ProviderDetails.DataBind();
    }
}