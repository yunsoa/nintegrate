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
using EAppointments.UI.ServiceAgents.AppointmentService;
using Microsoft.Practices.ObjectBuilder;
using System.Security.Principal;

public partial class Appointment_Summary : Page, ISummaryView
{
    private SummaryViewPresenter _presenter;
    private Appointment _currentAppointment;
    private bool _isNew;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            _presenter.OnViewInitialized();
        }
        _presenter.OnViewLoaded();
        apptDetails.Ubrn = _currentAppointment.UBRN;
        SetCreateButtonVisibility();
    }

    [CreateNew]
    public SummaryViewPresenter Presenter
    {
        get { return _presenter; }
        set
        {
            _presenter = value;
            _presenter.View = this;
        }
    }

    public Appointment CurrentAppointment
    {
        get { return _currentAppointment; }
        set { _currentAppointment = value; }
    }

    public bool IsNew
    {
        set { _isNew = value; }
    }

    public String Comments
    {
        get
        {
            return apptDetails.Comments;
        }
    }

    protected void OK_Click(object sender, ImageClickEventArgs e)
    {
       _presenter.OnFinish();
    }

    protected void lnkCreateAppointment_Click(object sender, EventArgs e)
    {
        _presenter.OnCreate();
    }

    private void SetCreateButtonVisibility()
    {
        IPrincipal currentPrincipal = System.Threading.Thread.CurrentPrincipal;

        CreateAppointment.Visible = (currentPrincipal.IsInRole("Referrer") || currentPrincipal.IsInRole("BMSAdmin"));
    }
}