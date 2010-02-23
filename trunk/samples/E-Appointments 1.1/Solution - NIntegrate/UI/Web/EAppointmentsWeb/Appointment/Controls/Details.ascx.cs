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
using System.Security.Principal;

public partial class Appointment_Details : UserControl, IAppointmentDetailsView
{
    private AppointmentDetailsViewPresenter _presenter;
    private int? _ubrn;
    private bool _editMode = false;
    private Appointment _currentAppointment;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            _presenter.OnViewInitialized();
        }
        BindData();
    } 

    [CreateNew]
    public AppointmentDetailsViewPresenter Presenter
    {
        get { return _presenter; }
        set
        {
            _presenter = value;
            _presenter.View = this;
        }
    }

    public int? Ubrn
    {
        get { return _ubrn; }
        set { _ubrn = value; }
    }
    
    public bool EditMode
    {
        get { return _editMode; }
        set { _editMode = value; }
    }

    public String Comments
    {
        get
        {
            TextBox commentsBox = (TextBox)AppointmentDetails.FindControl("Comments");
            return commentsBox.Text;
        }
    }

    public Appointment CurrentAppointment
    {
        set 
        {
            _currentAppointment = value;
            AppointmentDataSource.DataSource = value;             
        }
    }   

    public void BindData()
    {
        _presenter.OnViewLoaded();
        AppointmentDetails.DataBind();
        SetControlsVisibility();
    }

    private void SetControlsVisibility()
    {
        if (_editMode)
        {
            IPrincipal currentPrincipal = System.Threading.Thread.CurrentPrincipal;

            TextBox commentsBox = (TextBox)AppointmentDetails.FindControl("Comments");
            commentsBox.ReadOnly = !(currentPrincipal.IsInRole("Referrer") || currentPrincipal.IsInRole("BMSAdmin"));                       
        }

        if (_currentAppointment != null && (_currentAppointment.Status == AppointmentStatus.Cancelled))
        {
            HtmlTableRow cancellationDate = (HtmlTableRow)AppointmentDetails.FindControl("TRCancellationDate");
            cancellationDate.Visible = true;
            HtmlTableRow cancellationReason = (HtmlTableRow)AppointmentDetails.FindControl("TRCancellationReason");
            cancellationReason.Visible = true;
        }

        if (_currentAppointment != null && (_currentAppointment.Status == AppointmentStatus.Cancelled || _currentAppointment.Status == AppointmentStatus.Pending))
        {
            HtmlTableRow appointmentDate = (HtmlTableRow)AppointmentDetails.FindControl("TRAppointmentDate");
            appointmentDate.Visible = false;
            HtmlTableRow appointmentTime = (HtmlTableRow)AppointmentDetails.FindControl("TRAppointmentTime");
            appointmentTime.Visible = false;
        }
    }
}
