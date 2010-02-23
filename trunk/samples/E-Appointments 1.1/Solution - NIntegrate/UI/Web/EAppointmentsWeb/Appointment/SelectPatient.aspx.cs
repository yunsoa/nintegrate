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
using EAppointments.UI.ServiceAgents.DirectoryService;

public partial class Appointment_SelectPatient : Page, ISelectPatientView
{
    private SelectPatientViewPresenter _presenter;
    private Guid? _patientId;
    private bool _isNew;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            _presenter.OnViewInitialized();
        }
        _presenter.OnViewLoaded();
        SetVisibility();        
    }

    private void SetVisibility()
    {
        if (_patientId.HasValue)
        {
            pnlSelectPatient.Visible = false;
            pnlPatientDetails.Visible = true;
            if (_isNew)
                Change.Visible = true;
            patientDetails.PatientId = _patientId;
        }
        else
        {
            pnlSelectPatient.Visible = true;
            pnlPatientDetails.Visible = false;
            patientList.FirstName = FirstName.Text;
            patientList.LastName = LastName.Text;
        }
    }

    [CreateNew]
    public SelectPatientViewPresenter Presenter
    {
        get { return _presenter; }
        set
        {
            _presenter = value;
            _presenter.View = this;
        }
    }

    public Guid? PatientId
    {
        get { return _patientId; }
        set { _patientId = value; }
    }

    public bool IsNew
    {
        set { _isNew = value; }
    }

    protected void Search_Click(object sender, ImageClickEventArgs e)
    {
        patientList.Visible = true;
        patientList.FirstName = FirstName.Text;
        patientList.LastName = LastName.Text;
        patientList.BindData();
    }
    
    protected void Back_Click(object sender, ImageClickEventArgs e)
    {
        _presenter.OnBack();
    }

    protected void Next_Click(object sender, ImageClickEventArgs e)
    {
        if (_isNew)
        {
            if (!_patientId.HasValue)
            {
                _patientId = patientList.SelectedPatientId;
                if (!_patientId.HasValue)
                    return;
            }
        }
        _presenter.OnNext();
    }

    protected void Edit_Click(object sender, ImageClickEventArgs e)
    {
        _presenter.OnEdit();
    }
}
