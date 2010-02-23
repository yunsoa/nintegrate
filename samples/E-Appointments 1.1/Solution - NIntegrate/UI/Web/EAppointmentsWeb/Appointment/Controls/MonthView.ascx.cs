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
using System.Collections.Generic;

public partial class Appointment_MonthView : UserControl, IAppointmentListView
{
    private AppointmentListViewPresenter _presenter;
    private Appointment[] _appointments;
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

    public void BindData()
    {
        _presenter.OnViewLoaded();
    }

    public Appointment[] Appointments
    {
        set 
        { 
            _appointments = value;
        }
    }

    protected void MonthCalendar_DayRender(object sender, DayRenderEventArgs e)
    {
        CalendarDay currentDay = ((DayRenderEventArgs)e).Day;
        TableCell currentCell = ((DayRenderEventArgs)e).Cell;

        if (currentDay.IsOtherMonth)
        {
            currentCell.Controls.Clear();
            return;
        }

        Appointment[] currentDayAppointments = 
            Array.FindAll<Appointment>(_appointments, 
            delegate (Appointment appt) 
            {
                if (appt.StartDateTime.HasValue)
                    return appt.StartDateTime.Value.Date == currentDay.Date;
                else
                    return false;
            }
           );

        foreach (Appointment appt in currentDayAppointments)
        {
            ImageButton apptImage = new ImageButton();
            apptImage.ID = "monthAppt";
            apptImage.ImageUrl = "~/App_Themes/Default/Images/appointment.gif";
            apptImage.ToolTip = String.Format("{0} : {1} {2}", appt.UBRN, appt.Patient.FirstName, appt.Patient.LastName);
            apptImage.CssClass = "imagebutton";
            apptImage.Click += new ImageClickEventHandler(apptImage_Click);            
            apptImage.Attributes.Add("UBRN", appt.UBRN.ToString());
            Label label = new Label();
            label.Font.Size = FontUnit.Point(5);
            label.Text = String.Format("-{0:hh:mm tt}-{1:hh:mm tt}", appt.StartDateTime.Value, appt.EndDateTime.Value);
            currentCell.Controls.Add(new LiteralControl("<br>"));
            currentCell.Controls.Add(apptImage);
            currentCell.Controls.Add(label);            
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

    void apptImage_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton apptImage = (ImageButton)sender;
        int ubrn = Int32.Parse(apptImage.Attributes["UBRN"]);

        RowSelectedEventArgs args = new RowSelectedEventArgs(ubrn);

        // Fire the event handler giving the host page chance to handle 
        // this event in its context.
        OnRowSelected(args);
    } 
}
