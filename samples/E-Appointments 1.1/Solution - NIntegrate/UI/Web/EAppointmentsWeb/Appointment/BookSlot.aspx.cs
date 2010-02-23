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
using Microsoft.Practices.ObjectBuilder;
using EAppointments.UI.Modules.Views;
using System.Collections.Generic;

public partial class Appointment_BookSlot : Page, IBookSlotView
{
    private BookSlotViewPresenter _presenter;
    private Guid? _slotId;
    private Guid _providerId;
    private Guid _clinicTypeId;
    private bool _isNew;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            _presenter.OnViewInitialized();
            StartDate.Text = DateTime.Now.ToShortDateString();
            EndDate.Text = DateTime.Now.AddMonths(1).ToShortDateString();
            BindTimeVaules();
        }
        _presenter.OnViewLoaded();
        SetVisibility();
    }

    private void BindTimeVaules()
    {
        List<String> timeValues = new List<String>();
        DateTime dummyStartTime = DateTime.Now.Date;
        while (dummyStartTime < DateTime.Now.Date.AddDays(1))
        {
            timeValues.Add(dummyStartTime.ToString("hh:mm tt"));
            dummyStartTime = dummyStartTime.AddHours(1);
        }
        StartTime.DataSource = timeValues;
        StartTime.DataBind();
        EndTime.DataSource = timeValues;
        EndTime.DataBind();
        StartTime.SelectedIndex = 8;
        EndTime.SelectedIndex = 18;
    }

    private void SetVisibility()
    {
        Edit.Visible = false;
        if (_slotId.HasValue)
        {
            pnlSelectSlot.Visible = false;
            pnlSlotDetails.Visible = true;
            slotDetails.SlotId = _slotId;
            Edit.Visible = true;
        }
        else
        {
            pnlSelectSlot.Visible = true;
            pnlSlotDetails.Visible = false;
            SetSearchParameters();
        }
    }

    [CreateNew]
    public BookSlotViewPresenter Presenter
    {
        get { return _presenter; }
        set
        {
            _presenter = value;
            _presenter.View = this;
        }
    }

    public Guid? SlotId
    {
        get { return _slotId; }
        set { _slotId = value; }
    }

    public Guid ProviderId
    {
        set { _providerId = value; }
    }

    public Guid ClinicTypeId
    {
        set { _clinicTypeId = value; }
    }

    public bool IsNew
    {
        set { _isNew = value; }
    }

    protected void Search_Click(object sender, ImageClickEventArgs e)
    {
        slotList.Visible = true;
        SetSearchParameters();
        slotList.BindData();
    }

    private int GetWeekDaySelection()
    { 
        int weekDays = 0;
        weekDays |= chkSunday.Checked ? 2 : 0;
        weekDays |= chkMonday.Checked ? 4 : 0;
        weekDays |= chkTuesday.Checked ? 8 : 0;
        weekDays |= chkWednesday.Checked ? 16 : 0;
        weekDays |= chkThursday.Checked ? 32 : 0;
        weekDays |= chkFriday.Checked ? 64 : 0;
        weekDays |= chkSaturday.Checked ? 128 : 0;        
        return weekDays;
    }

    protected void Back_Click(object sender, ImageClickEventArgs e)
    {
        _presenter.OnBack();
    }

    protected void Next_Click(object sender, ImageClickEventArgs e)
    {
        _slotId = slotList.SelectedSlotId;           
        if (!_slotId.HasValue)
            return;
        _presenter.OnNext();
    }

    protected void Edit_Click(object sender, ImageClickEventArgs e)
    {
        _presenter.OnEdit();
    }

    private void SetSearchParameters()
    {
        if (slotList.Visible)
        {
            slotList.StartDate = DateTime.Parse(StartDate.Text + " " + StartTime.SelectedValue);
            slotList.EndDate = DateTime.Parse(EndDate.Text + " " + EndTime.SelectedValue);
            slotList.ProviderId = _providerId;
            slotList.ClinicTypeId = _clinicTypeId;
            slotList.WeekDays = GetWeekDaySelection();
        }
    }

}
