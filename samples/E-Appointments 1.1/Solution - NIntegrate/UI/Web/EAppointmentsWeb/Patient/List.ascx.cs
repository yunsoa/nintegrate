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
using EAppointments.UI.ServiceAgents.DirectoryService;
using EAppointments.UI.Modules;
using EAppointments.UI.Modules.Views;
using Microsoft.Practices.ObjectBuilder;
using Microsoft.Practices.CompositeWeb.Interfaces;
using Microsoft.Practices.CompositeWeb;

public partial class Patient_List :  UserControl, IPatientListView
{
    private PatientListViewPresenter _presenter;
    private String _firstName;
    private String _lastName;

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
        PatientDataSource.DataBind();        
    }

    [CreateNew]
    public PatientListViewPresenter Presenter
    {
        get { return _presenter; }
        set
        {
            _presenter = value;
            _presenter.View = this;
        }
    }

    public String FirstName
    {
        get { return _firstName; }
        set { _firstName = value; }
    }

    public String LastName
    {
        get { return _lastName; }
        set { _lastName = value; }
    }    

    public Patient[] Patients
    {
        set { PatientDataSource.DataSource = value; }
    }

    public Guid? SelectedPatientId
    {
        get
        {
            if (PatientGrid.SelectedIndex >= 0)
                return (Guid)PatientGrid.SelectedDataKey.Value;
            else
                return null;
        }
    }

    public void BindData()
    {
        _presenter.OnViewLoaded();
        PatientGrid.DataBind();
    }
}
