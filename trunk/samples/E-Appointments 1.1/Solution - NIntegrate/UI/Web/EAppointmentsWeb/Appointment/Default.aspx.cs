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

public partial class Appointment_Default : Page, IAppointmentDashboardView
{
    private AppointmentDashboardViewPresenter _presenter;
    private Appointment _selectedAppointment;
    private DateTime? _startDateTime;
    private DateTime? _endDateTime;
    private int _status;

    protected void Page_Load(object sender, EventArgs e)
    {
        Initialize();

        if (!this.IsPostBack)
        {
            _presenter.OnViewInitialized();
            txtStartDate.Text = DateTime.Now.ToShortDateString();
            txtEndDate.Text = DateTime.Now.AddMonths(1).ToShortDateString();
        }
        SetCreateButtonVisibility();
        UpdateViews();
    }

    private void Initialize()
    {
        this.appointmentList.RowSelected += new EventHandler<RowSelectedEventArgs>(OnAppointmentRowSelected);
        this.monthView.RowSelected += new EventHandler<RowSelectedEventArgs>(OnAppointmentRowSelected);
        this.weekView.RowSelected += new EventHandler<RowSelectedEventArgs>(OnAppointmentRowSelected);
    }

    [CreateNew]
    public AppointmentDashboardViewPresenter Presenter
    {
        get { return _presenter; }
        set
        {
            _presenter = value;
            _presenter.View = this;
        }
    }

    public Appointment SelectedAppointment
    {
        set { _selectedAppointment = value; }
        get { return _selectedAppointment; }
    }

    protected void OnAppointmentRowSelected(object sender, RowSelectedEventArgs e)
    {
        int ubrn = (int)e.SelectedRowDataItem;
        _presenter.OnAppointmentRowSelected(ubrn);
        patientDetails.PatientId = _selectedAppointment.Patient.Id;
        patientDetails.BindData();
        appointmentDetails.Ubrn = _selectedAppointment.UBRN;
        appointmentDetails.BindData();
        ActionAccordionPane.Visible = true;
        SetActionButtonsVisibility();
        pnlDetails.Update();
    }

    // TODO: Move this to presenter
    private void SetCreateButtonVisibility()
    {
        IPrincipal currentPrincipal = System.Threading.Thread.CurrentPrincipal;

        CreateAppointment.Visible = (currentPrincipal.IsInRole("Referrer") || currentPrincipal.IsInRole("BMSAdmin"));
    }

    // TODO: Move this to presenter
    private void SetActionButtonsVisibility()
    {
        IPrincipal currentPrincipal = System.Threading.Thread.CurrentPrincipal;
        
        Book.Visible = (_selectedAppointment.Status == AppointmentStatus.Pending 
                || _selectedAppointment.Status == AppointmentStatus.Rejected)
                && (currentPrincipal.IsInRole("Referrer") || currentPrincipal.IsInRole("BMSAdmin") || currentPrincipal.IsInRole("Patient"));

        Cancel.Visible = (_selectedAppointment.Status == AppointmentStatus.Approved
                || _selectedAppointment.Status == AppointmentStatus.Rejected
                || _selectedAppointment.Status == AppointmentStatus.Booked)
                && (currentPrincipal.IsInRole("Referrer") || currentPrincipal.IsInRole("BMSAdmin") || currentPrincipal.IsInRole("Patient"));
       
        ReBook.Visible = (_selectedAppointment.Status == AppointmentStatus.Booked)
                && (currentPrincipal.IsInRole("Referrer") || currentPrincipal.IsInRole("BMSAdmin") || currentPrincipal.IsInRole("Patient"));

        Approve.Visible = (_selectedAppointment.Status == AppointmentStatus.Booked
                || _selectedAppointment.Status == AppointmentStatus.Rejected) && (currentPrincipal.IsInRole("Provider"));

        Reject.Visible = (_selectedAppointment.Status == AppointmentStatus.Booked
                || _selectedAppointment.Status == AppointmentStatus.Approved) && (currentPrincipal.IsInRole("Provider"));

        Delete.Visible = (_selectedAppointment.Status == AppointmentStatus.Pending               
                || _selectedAppointment.Status == AppointmentStatus.Cancelled)
                && (currentPrincipal.IsInRole("Referrer") || currentPrincipal.IsInRole("BMSAdmin"));

        Edit.Visible = (currentPrincipal.IsInRole("Referrer") || currentPrincipal.IsInRole("BMSAdmin"));

    }

    protected void DateFilter_Click(object sender, ImageClickEventArgs e)
    {
        // UpdateViews();
    }

    protected void monthCalendar_SelectionChanged(object sender, EventArgs e)
    {

        // UpdateViews();
    }

    protected void ViewFilter_Click(object sender, ImageClickEventArgs e)
    {
        // UpdateViews();
    }

    private void SetCurrentView()
    {
        if (radAll.Checked)
            _status = (int)(AppointmentStatus.Booked | AppointmentStatus.Pending | AppointmentStatus.Approved
                      | AppointmentStatus.Rejected | AppointmentStatus.Cancelled);
        else if (radBooked.Checked)
            _status = (int)AppointmentStatus.Booked;
        else if (radPending.Checked)
            _status = (int)AppointmentStatus.Pending;
        else if (radApproved.Checked)
            _status = (int)AppointmentStatus.Approved;
        else if (radRejected.Checked)
            _status = (int)AppointmentStatus.Rejected;
        else if (radCancelled.Checked)
            _status = (int)AppointmentStatus.Cancelled;
    }

    private void SetDateRange()
    {
        DateTime startDate, endDate;
        if (DateTime.TryParse(txtStartDate.Text, out startDate))
            _startDateTime = startDate;
        if (DateTime.TryParse(txtEndDate.Text, out endDate))
            _endDateTime = endDate;
    }

    private void UpdateViews()
    {
        SetDateRange();
        SetCurrentView();
        appointmentList.StartDateTime = _startDateTime;
        appointmentList.EndDateTime = _endDateTime;
        appointmentList.Status = _status;
        monthView.StartDateTime = _startDateTime;
        monthView.EndDateTime = _endDateTime;
        monthView.Status = _status;
        weekView.StartDateTime = _startDateTime;
        weekView.EndDateTime = _endDateTime;
        weekView.Status = _status;
    }

    protected void lnkCreateAppointment_Click(object sender, EventArgs e)
    {
        _presenter.OnCreate();
    }

    protected void Edit_Click(object sender, ImageClickEventArgs e)
    {
        int? ubrn = appointmentList.SelectedUbrn;
        if (ubrn.HasValue)
            _presenter.OnEdit((int)ubrn);
    }

    protected void Book_Click(object sender, ImageClickEventArgs e)
    {
        int? ubrn = appointmentList.SelectedUbrn;
        if (ubrn.HasValue)
            _presenter.OnBook((int)ubrn);
    }

    protected void ReBook_Click(object sender, ImageClickEventArgs e)
    {
        int? ubrn = appointmentList.SelectedUbrn;
        if (ubrn.HasValue)
            _presenter.OnReBook((int)ubrn);
    }

    protected void Cancel_Click(object sender, ImageClickEventArgs e)
    {
        int? ubrn = appointmentList.SelectedUbrn;
        if (ubrn.HasValue)
            _presenter.OnCancel((int)ubrn);
    }

    protected void Delete_Click(object sender, ImageClickEventArgs e)
    {
        int? ubrn = appointmentList.SelectedUbrn;
        if (ubrn.HasValue)
            _presenter.OnDelete((int)ubrn);
    }

    protected void Approve_Click(object sender, ImageClickEventArgs e)
    {
        int? ubrn = appointmentList.SelectedUbrn;
        if (ubrn.HasValue)
            _presenter.OnApprove((int)ubrn);
    }

    protected void Reject_Click(object sender, ImageClickEventArgs e)
    {
        int? ubrn = appointmentList.SelectedUbrn;
        if (ubrn.HasValue)
            _presenter.OnReject((int)ubrn);
    }
}
