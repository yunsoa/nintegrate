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
using EAppointments.UI.ServiceAgents.AppointmentService;

public partial class Appointment_Action : Page, IActionView
{
    private ActionViewPresenter _presenter;
    private Appointment _currentAppointment;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            _presenter.OnViewInitialized();
        }
        _presenter.OnViewLoaded();
        SetVisibility();
        apptDetails.Ubrn = _currentAppointment.UBRN;
    }

    private void SetVisibility()
    {
        String command = Request.QueryString["command"];
        switch (command.ToLower())
        {
            case "approve": Approve.Visible = true; break;
            case "reject": Reject.Visible = true; break;
            case "cancel": Cancel.Visible = true;
                CancellationDiv.Visible = true;
                break;
            case "delete": Delete.Visible = true; break;
        }
    }

    [CreateNew]
    public ActionViewPresenter Presenter
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

    public String Reason
    {
        get { return CancellationReason.Text; }
    }

    protected void Back_Click(object sender, ImageClickEventArgs e)
    {
        _presenter.OnBack();
    }

    protected void Approve_Click(object sender, ImageClickEventArgs e)
    {
        _presenter.OnApprove();
    }

    protected void Reject_Click(object sender, ImageClickEventArgs e)
    {
        _presenter.OnReject();
    }

    protected void Cancel_Click(object sender, ImageClickEventArgs e)
    {
        _presenter.OnCancel();
    }

    protected void Delete_Click(object sender, ImageClickEventArgs e)
    {
        _presenter.OnDelete();
    } 
}