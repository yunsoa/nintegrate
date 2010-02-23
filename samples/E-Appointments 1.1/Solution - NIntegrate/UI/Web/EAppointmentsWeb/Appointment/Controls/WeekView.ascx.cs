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
using EAppointments.UI.Modules.Views;
using Microsoft.Practices.ObjectBuilder;
using System.Collections.Generic;

public partial class Appointment_WeekView : UserControl, IAppointmentListView
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

    public void BindData()
    {
        _presenter.OnViewLoaded();
        List<DateTime> days = new List<DateTime>();
        if (!StartDateTime.HasValue)
        {
            StartDateTime = DateTime.Now;
        }
        EndDateTime = StartDateTime.Value.AddDays(7);
        for (DateTime date = StartDateTime.Value; date.CompareTo(EndDateTime.Value) < 0; date = date.AddDays(1))
        {
            days.Add(date);
        }
        AppointmentDataSource.DataSource = days;
        AppointmentDataSource.DataBind();
        WeekRepeater.DataBind();
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
        set { _appointments = value; }
    }

    protected void WeekRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header || e.Item.ItemType == ListItemType.Footer)
            return;

        DateTime currentDate = (DateTime)e.Item.DataItem;

        Appointment[] currentDayAppointments =
           Array.FindAll<Appointment>(_appointments,
           delegate(Appointment appt)
           {
               if (appt.StartDateTime.HasValue)
                   return appt.StartDateTime.Value.Date == currentDate.Date;
               else
                   return false;
           }
          );

        Label noApptsLabel = (Label)e.Item.FindControl("lblNoAppointments");
        if (currentDayAppointments.Length == 0)
        {
            noApptsLabel.Visible = true;
            return;
        }

        Panel apptPanel = (Panel)e.Item.FindControl("pnlAppointments");
        apptPanel.Visible = true;

        foreach (Appointment appt in currentDayAppointments)
        {
            ImageButton apptImage = new ImageButton();
            apptImage.ImageUrl = "~/App_Themes/Default/Images/appointment.gif";
            apptImage.ToolTip = String.Format("{0} : {1} {2}", appt.UBRN, appt.Patient.FirstName, appt.Patient.LastName);
            apptImage.CssClass = "imagebutton";
            apptImage.Click += new ImageClickEventHandler(apptImage_Click);
            apptImage.Attributes.Add("UBRN", appt.UBRN.ToString());
            apptPanel.Controls.Add(apptImage);
            apptPanel.Controls.Add(new LiteralControl(String.Format("{0:hh:mm tt} - {1:hh:mm tt}({2} - {3})",
                                appt.StartDateTime.Value, appt.EndDateTime.Value, appt.UBRN, appt.Provider.Name)));
            apptPanel.Controls.Add(new LiteralControl("<br>"));
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


